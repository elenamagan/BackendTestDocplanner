using BackendTestDocplanner.Services.Slot;
using BackendTestDocplanner.Services.Slot.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

namespace BackendTestDocplanner
{
    public static class Program
    {
        /// <summary>
        /// Application's configuration, loaded on Main() from appsettings.json
        /// </summary>
        private static IConfiguration? Configuration { get; set; }

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

            var builder = WebApplication.CreateBuilder();

            // Slot service configuration
            builder.Services.AddHttpClient<SlotService>(client =>
            {
                string username = Configuration["SlotService:Username"] ?? throw new InvalidOperationException("Please provide a username for the Slot Service.");
                string password = Configuration["SlotService:Password"] ?? throw new InvalidOperationException("Please provide a username for the Slot Service.");

                client.BaseAddress = new Uri("https://draliatest.azurewebsites.net/");
                var authHeaderValue = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            });

            // API and swagger configuration
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Docplanner Backend Test API", Version = "v1" });
                c.EnableAnnotations();
                c.SchemaFilter<TakeSlotRequestExample>();
            });

            var apiHost = builder.Build();
            apiHost.UseSwagger();
            apiHost.UseSwaggerUI();
            apiHost.UseHttpsRedirection();
            apiHost.UseAuthorization();
            apiHost.MapControllers();

            // Runs API in another thread
            Task.Run(() => apiHost.Run());

            // Starts WindowsForms UI
            // MUST BE OPEN for the API to keep running
            Application.Run(new WeeklySlots());
        }
    }
}