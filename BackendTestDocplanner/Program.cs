using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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

                    webBuilder.UseStartup<Startup>();
                });
    }

}