using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

using Shares.Library;
using Shares.ConsoleApp;

namespace Shares.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // basic input validation
            if (args == null || args.Length != 1)
            {
                var exeName = System.AppDomain.CurrentDomain.FriendlyName;
                Console.WriteLine($"\nUsage: {exeName} \"1,2,3,4\"\n");
                Environment.Exit(0);
            }

            var loggerFactory = BuildLoggerFactory();
            var logger = loggerFactory.CreateLogger<Program>();

            try {
                var serviceProvider = BuildServices(loggerFactory);

                // TODO: validation on input
                var data = args[0];

                var app = serviceProvider.GetService<IConsoleApplication>() as IConsoleApplication;
                app.Run(data);
            }
            catch(Exception e)
            {
                logger.LogError($"{e.Message}");
                Console.WriteLine($"Error {e.Message}");
            }
        }

        static ILoggerFactory BuildLoggerFactory()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"));
                builder.AddConsole();
            });

            return loggerFactory;
        }

        private static ServiceProvider BuildServices(ILoggerFactory loggerFactory)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILoggerFactory>(loggerFactory)
                .AddScoped<ITradeService, SingleTradeService>()
                .AddScoped<IConsoleApplication, ConsoleApplication>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}