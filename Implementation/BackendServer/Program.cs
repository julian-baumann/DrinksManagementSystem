using BackendServer.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BackendServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppCore.Initialize();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
