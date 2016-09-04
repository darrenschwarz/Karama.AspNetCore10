using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SwashbuckleExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var e = Directory.GetCurrentDirectory();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
