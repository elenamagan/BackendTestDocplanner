using BackendTestDocplanner.Services.Slot;
using BackendTestDocplanner.Services.Slot.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

namespace BackendTestDocplanner
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Runs API on a separate thread
            Task webTask = Task.Run(() => host.Run());

            // Starts WindowsForms UI
            // MUST BE OPEN for the API to keep running
            var form = new WeeklySlots();
            Application.Run(form);

            await webTask;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // In Startup.cs: API configuration and launch
                    // · Adds swagger with defined controller's endpoints
                    // · Uses HTTPS with "safe" certificate from ./Certificates (in this case, self-signed, but easily changed)
                    // · Runs API in a separate threat to launch the WindowsForms UI on start

                    webBuilder.UseStartup<Startup>();
                });
    }

}