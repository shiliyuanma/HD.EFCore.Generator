using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace HD.EFCore.SqlServerGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder().AddEnvironmentVariables().AddCommandLine(args).Build())
                .UseContentRoot(AppContext.BaseDirectory)
                .UseStartup<Startup>()
                .Build();
    }
}
