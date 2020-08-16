namespace Shares.Library
{
    public class TradeDataSet {
        public TradeDataSet(float [] data)
        {
            Data = data;
        }

        public float[] Data { get; private set; }
    }
}
