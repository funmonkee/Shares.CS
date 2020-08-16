namespace Shares.Library
{
    // model a buy and sell transaction
    public class Trade 
    {
        public SharePrice Buy { get; set; }
        public SharePrice Sell {get; set;}

        public float Profit => this.Sell.Price - this.Buy.Price;

        public Trade(SharePrice buy, SharePrice sell)
        {
            this.Buy = buy;
            this.Sell = sell;
        }
    }
}