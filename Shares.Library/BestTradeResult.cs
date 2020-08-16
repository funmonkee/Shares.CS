using System;
using System.Collections.Generic;
using System.Linq;

namespace Shares.Library
{
    public class BestTradeResult
    {
        public List<Trade> Trades { get; private set; }

        public bool HasTrades => this.Trades.Any();

        private BestTradeResult()
        {
            this.Trades = new List<Trade>();
        }

        public BestTradeResult(SharePrice buy, SharePrice sell)
        {
            this.Trades = new List<Trade> { new Trade(buy, sell) };
        }

        public static BestTradeResult BuildEmptyResult()
        {
            return new BestTradeResult();
        }
    }
}