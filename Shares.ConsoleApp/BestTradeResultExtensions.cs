using Shares.Library;

namespace Shares.ConsoleApp
{
    public static class BestTradeResultExtensions {
        public static string Print(this SharePrice res){
            return $"{res.Day}({res.Price:F2})";
        }
    }
}