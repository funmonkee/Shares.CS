namespace Shares.Library
{
    // model of the share price for a particular day in a dataset
    public class SharePrice 
    {
        public int Day { get; private set; }
        public float Price { get; private set; }

        public SharePrice(int day, float price)
        {
            this.Day = day;
            this.Price = price;
        }
    }
}