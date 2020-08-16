using System;
using Microsoft.Extensions.Logging;

namespace Shares.Library
{
    public class SingleTradeService : ITradeService
    {
        private readonly ILogger<SingleTradeService> logger;

        public SingleTradeService(ILogger<SingleTradeService> logger)
        {
            this.logger = logger;
        }

        public SingleTradeService(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<SingleTradeService>();
        }

        public BestTradeResult GetBestTrade(TradeDataSet dataSet)
        {
            var (buyDayIndex, sellDayIndex) = this.GetBestTrades(dataSet.Data);

            var noResult = buyDayIndex == sellDayIndex;
            if (noResult)
            {
                this.logger.LogInformation("no best trade results");
                return BestTradeResult.BuildEmptyResult();
            }
            else
            {
                this.logger.LogInformation("successful result");
                return new BestTradeResult(
                    new SharePrice(buyDayIndex + 1, dataSet.Data[buyDayIndex]),
                    new SharePrice(sellDayIndex + 1, dataSet.Data[sellDayIndex]));
            }
        }

        private Tuple<int,int> GetBestTrades(float[] values){
            float maxProfit = 0.0F;
            int minDayIndex = 0;

            int buyDayIndex = 0;
            int sellDayIndex = 0;

            for (int i = 0; i < values.Length; i++)
            {
                var minDayPrice = values[minDayIndex];
                var stepProfit = values[i] - minDayPrice;

                var isNewMinPrice = values[i] < minDayPrice;

                if (isNewMinPrice)
                {
                    minDayIndex = i;
                }
                else
                {
                    if (stepProfit > maxProfit)
                    {
                        this.logger.LogInformation($"profit found - buy:{minDayIndex}, sell:{i}");

                        maxProfit = stepProfit;
                        buyDayIndex = minDayIndex;
                        sellDayIndex = i;
                    }
                }
            }

            return new Tuple<int, int>( buyDayIndex, sellDayIndex);
        }
    }
}