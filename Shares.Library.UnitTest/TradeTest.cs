using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

using Shares.Library;

namespace Shares.Library.UnitTest
{
    public class TradeTest 
    {
        [Fact]
        public void GivenBuyPriceLessThanSellPrice_WhenProfitCalled_ThenExpectPositiveProfit() 
        {
            // arrange
            var buyPrice = new SharePrice(1, 2.0F);
            var sellPrice = new SharePrice(3, 4.0F);
            var trade = new Trade(buyPrice, sellPrice);

            // act
            var result = trade.Profit;

            // assert
            Assert.True(result > 0);
            Assert.Equal(2.0F, result);       
        }

        [Fact]
        public void GivenSellPriceLessThanBuyPrice_WhenProfitCalled_ThenExpectNegativeProfit() 
        {
            // arrange
            var buyPrice = new SharePrice(5, 4.0F);
            var sellPrice = new SharePrice(8, 1.0F);
            var trade = new Trade(buyPrice, sellPrice);

            // act
            var result = trade.Profit;

            // assert
            Assert.True(result < 0);
            Assert.Equal(-3.0F, result);       
        }
    }
}