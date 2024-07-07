using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTestDocplanner
{
    internal static class Program
    {
        /// <summary>
        /// Application's configuration, loaded on Main() from appsettings.json
        /// </summary>
        private static IConfiguration Configuration { get; set; }

        static void Main()
        {
            // API configuration and launch
            // · Adds swagger with defined controller's endpoints
            // · Uses HTTPS with "safe" certificate from ./Certificates (in this case, self-signed, but easily changed)
            // · Runs API in a separate threat to also launch a WindowsForms UI on start

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var apiBuilder = WebApplication.CreateBuilder();
            apiBuilder.Services.AddControllers();
            apiBuilder.Services.AddEndpointsApiExplorer();
            apiBuilder.Services.AddSwaggerGen();

            var apiHost = apiBuilder.Build();
            apiHost.UseSwagger();
            apiHost.UseSwaggerUI();
            apiHost.UseHttpsRedirection();
            apiHost.UseAuthorization();
            apiHost.MapControllers();

            Task.Run(() => apiHost.Run());

            // Starts WindowsForms UI
            Application.Run(new WeeklySlots());
        }
    }
}