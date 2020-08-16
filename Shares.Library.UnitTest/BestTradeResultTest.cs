using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

using Shares.Library;

namespace Shares.Library.UnitTest
{
    public class BestTradeResultTest
    {
        [Fact]
        public void GivenBuildEmptyResult_WhenHasTradesCalled_ThenExpectNoTrades()
        {
            // arrange
            var sut = BestTradeResult.BuildEmptyResult();

            // act & assert
            Assert.False(sut.HasTrades);
        }

                [Fact]
        public void GivenValidTradeResult_WhenHasTradesCalled_ThenExpectToHaveTrades()
        {
            // arrange
            var buyPrice = new SharePrice(1, 2.0F);
            var sellPrice = new SharePrice(4, 3.5F);
            var sut = new BestTradeResult(buyPrice, sellPrice);

            // act & assert
            Assert.True(sut.HasTrades);
        }
    }
}