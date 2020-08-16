namespace Shares.Library
{
    // contract for the best trade
    public interface ITradeService
    {
        BestTradeResult GetBestTrade(TradeDataSet data);
    }
}