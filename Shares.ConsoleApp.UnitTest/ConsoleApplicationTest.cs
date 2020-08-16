using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using Xunit;
using Moq;

using Shares.Library;
using Shares.ConsoleApp;

namespace Shares.ConsoleApp.UnitTest
{
    public class ConsoleApplicationTest
    {
        [Fact]
        public void GivenSampleData_WhenRunCalled_ThenOutputAsExpected()
        {
            // arrange
            var mockLogger = new Mock<ILogger<ConsoleApplication>>();
            var mockService = new Mock<ITradeService>();
            
            var buy = new SharePrice(0,1.0F);
            var sell = new SharePrice(3, 4.0F);
            var bestResult = new BestTradeResult(buy,sell);

            mockService
                .Setup(ms => ms.GetBestTrade(It.Is<TradeDataSet>(d => true)))
                .Returns(bestResult);
            
            var output = new StringWriter();
            Console.SetOut(output);

            var sut = new ConsoleApplication(mockLogger.Object, mockService.Object);

            // act
            sut.Run("1,2,3,4");

            // assert
            Assert.Equal("0(1.00),3(4.00) \n", output.ToString());
        }
    }
}
