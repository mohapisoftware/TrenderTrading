using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public interface iTrenderMtApiService
    {
        Task<Boolean> Connect();

        Task<int> OpBuy(string symbol, double volume, int slippage);
        Task<int> OpBuy(string symbol, double volume, int slippage, double stoploss, double takeprofit);
        Task<int> CloseTrade(int tradeID);
        Task<int> OpSell(string symbol, double volume, int slippage);
        Task<int> OpSell(string symbol, double volume, int slippage, double stoploss, double takeprofit);
        Task<TrenderConnectionState> GetConnectionState();
        Task<double> GetCurrentPrice();
        Task<Boolean> Disconnect();
    }
}
