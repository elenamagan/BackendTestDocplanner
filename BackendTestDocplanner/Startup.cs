using BackendTestDocplanner.Services.Slot;
using BackendTestDocplanner.Services.Slot.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

namespace BackendTestDocplanner
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Slot service configuration
            services.AddHttpClient<SlotService>(client =>
            {
                string username = Configuration["SlotService:Username"] ?? throw new InvalidOperationException("Please provide a username for the Slot Service.");
                string password = Configuration["SlotService:Password"] ?? throw new InvalidOperationException("Please provide a username for the Slot Service.");

                client.BaseAddress = new Uri("https://draliatest.azurewebsites.net/");
                var authHeaderValue = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            });

            // API and swagger configuration
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Docplanner Backend Test API", Version = "v1" });
                c.EnableAnnotations();
                c.SchemaFilter<TakeSlotRequestExample>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            // HTTPS pipeline for API
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Docplanner Backend Test API v1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Runs API in another thread
            app.Run(async context => { await Task.Delay(0); });
        }
    }
}
