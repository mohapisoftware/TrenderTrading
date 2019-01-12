using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public enum TrenderTradeOperation
    {
        OpBuy=1
            ,OpSell=2
            ,OpNeutral=3
    }

    public enum TrenderConnectionState
    {
        Disconnected=1,
        Connecting=2,
        Failed=3,
        Connected=4
    }

    public class TradeParameters
    {
        public string Symbol { get; set; }
        public double TakeProfit { get; set; }
        public double StopLoss { get; set; }
        public double Volume { get; set; }
        public int Slippage { get; set; }

        public TradeParameters(string Symbol, double Volume, int Slippage)
        {
            this.Symbol = Symbol;
            this.Volume = Volume;
            this.Slippage = Slippage;
        }

        public TradeParameters(string Symbol, double Volume, int Slippage, double TakeProfit, double StopLoss)
        {
            this.Symbol = Symbol;
            this.Volume = Volume;
            this.Slippage = Slippage;
            this.TakeProfit = TakeProfit;
            this.StopLoss = StopLoss;
        }
    }
}
