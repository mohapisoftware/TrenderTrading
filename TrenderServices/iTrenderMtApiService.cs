using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtApi;
namespace Trender
{
    public interface iTrenderMtApiService
    {
        Task<Boolean> Connect();

        Task<int> OpBuy(string symbol, double volume, int slippage);
        Task<int> OpBuy(string symbol, double volume, int slippage, double stoploss, double takeprofit);
        Task<Boolean> CloseTrade(int tradeID, int slippage);
        Task<int> OpSell(string symbol, double volume, int slippage);
        Task<int> OpSell(string symbol, double volume, int slippage, double stoploss, double takeprofit);
        Task<TrenderConnectionState> GetConnectionState();
        Task<double> GetCurrentPrice(string symbol);
        Task<bool> isTradingEnabled();
        Task<bool> DisableTrading();
        Task<List<MqlRates>> GetRates(string symbol, ENUM_TIMEFRAMES timeframes, int startpos, int count);
        Task<double> GetATR(string symbol, ENUM_TIMEFRAMES timeframes, int period, int shift);
        Task<List<MtOrder>> GetOrders();
        Task<Boolean> Disconnect();
    }
}
