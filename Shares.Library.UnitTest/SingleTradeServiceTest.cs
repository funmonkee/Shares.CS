using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

using Shares.Library;

namespace Shares.Library.UnitTest
{
    public class SingleTradeServiceTest
    {
        [Fact]
        public void GivenDecsendingPriceDataSet_WhenGetbestTradeCalled_ThenExpectNoResults()
        {
            // arrange
            var arrayOfEndOfDayPrices = new float[] { 33, 32, 31, 29, 28, 27, 19, 15, 6, 1 };
            var dataSet = new TradeDataSet(arrayOfEndOfDayPrices);
            var sut = BuildSimpleShareEngine();

            // act
            var result = sut.GetBestTrade(dataSet);

            // assert
            Assert.False(result.HasTrades);
            Assert.Empty(result.Trades);
        }

        [Fact]
        public void GivenAscendingEndOfDayPrices_WhenGetBestTradeCalled_ThenExpectOneResult()
        {
            // arrange 
            var arrayOfEndOfDayPrices = new float[]{ 1.0F, 2.0F, 3.5F, 9, 18, 27, 39, 45, 56, 61};
            var dataSet = new TradeDataSet(arrayOfEndOfDayPrices);
            var sut = BuildSimpleShareEngine();

            // act
            var result = sut.GetBestTrade(dataSet);

            // assert
            Assert.Single(result.Trades);
        }



        [Theory]
        [InlineData(10.40F, 20, 21, "22.74,22.27,20.61,26.15,21.68,21.51,19.66,24.11,20.63,20.96,26.56,26.67,26.02,27.20,19.13,16.57,26.71,25.91,17.51,15.79,26.19,18.57,19.03,19.02,19.97,19.04,21.06,25.94,17.03,15.61")]
        [InlineData(12.11F, 15, 21, "18.93,20.25,17.05,16.59,21.09,16.22,21.43,27.13,18.62,21.31,23.96,25.52,19.64,23.49,15.28,22.77,23.1,26.58,27.03,23.75,27.39,15.93,17.83,18.82,21.56,25.33,25,19.33,22.08,24.03")]
        public void GivenSampleDataSet_WhenSingleGetBestTrade_ThenExpected(float profit, int buyDayIndex, int sellDayIndex, string data)
        {
            // arrange
            var arrayOfEndOfDayPrices = data.Split(',').ToList<string>().Select(x => float.Parse(x)).ToArray(); // TODO: convert to helper
            var dataSet = new TradeDataSet(arrayOfEndOfDayPrices);
            var sut = BuildSimpleShareEngine();

            // act
            var result = sut.GetBestTrade(dataSet);

            // assert
            Assert.Single(result.Trades);
            Assert.True(result.HasTrades);

            var trade = result.Trades.First();
            Assert.Equal(buyDayIndex, trade.Buy.Day);
            Assert.Equal(sellDayIndex, trade.Sell.Day);

            Assert.Equal(TestHelpers.Round(profit), TestHelpers.Round(trade.Profit));
        }

        private static SingleTradeService BuildSimpleShareEngine()
        {
            var mockLogger = new Mock<ILogger<SingleTradeService>>();
            var sut = new SingleTradeService(mockLogger.Object);
            return sut;
        }
    }
}