using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

using Shares.Library;

namespace Shares.ConsoleApp
{
    public class ConsoleApplication : IConsoleApplication
    {
        private readonly ILogger<ConsoleApplication> logger;
        public readonly ITradeService TradeService;

        public ConsoleApplication(ILoggerFactory loggerFactory, ITradeService tradeService)
        {
            this.TradeService = tradeService;
            this.logger = loggerFactory.CreateLogger<ConsoleApplication>();
        }

        public void Run(string inp)
        {
            this.logger.LogInformation("ConsoleApplication Run");

            var pricesStringArray = inp.Split(',').ToList<string>();
            var pricesAsFloatArray = pricesStringArray.Select(x => float.Parse(x)).ToArray();
            var tradeDataSet = new TradeDataSet(pricesAsFloatArray);

            this.logger.LogInformation("ConsoleApplication calling GetBestTrade");
            var result = this.TradeService.GetBestTrade(tradeDataSet);

            this.logger.LogInformation("ConsoleApplication output result");
            if (!result.HasTrades)
            {
                var message = "No trades found";
;                this.logger.LogInformation($"{message}");
                Console.WriteLine(message);
            }
            else
            {
                var message = "Trade found";
                this.logger.LogInformation($"{message}");
                this.DisplayResults(result);
            }
        }

        private void DisplayResults(BestTradeResult result)
        {
            foreach (var trade in result.Trades)
            {
                Console.Write($"{trade.Buy.Print()},{trade.Sell.Print()} ");
            }
            Console.WriteLine();
        }
    }
}