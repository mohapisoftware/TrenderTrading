using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtApi;

namespace Trender
{
    public class TrenderMtApiServiceMock : iTrenderMtApiService
    {
        private string host;
        private int port;
        private Boolean TradeEnabled = true;

        public TrenderMtApiServiceMock(string host, int port)
        {
            this.host = "127.0.0.1";
            this.port = 1443;

        }
        public Task<int> CloseTrade(int tradeID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CloseTrade(int tradeID, int slippage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Connect()
        {
            return Task.FromResult(true);
        }

        public Task<bool> DisableTrading()
        {
            TradeEnabled = false;
            return Task.FromResult(true);
        }

        public Task<bool> Disconnect()
        {
            return Task.FromResult(true);
        }

        public Task<Trender.TrenderConnectionState> GetConnectionState()
        {
            return Task.FromResult(TrenderConnectionState.Connected);
        }

        public Task<double> GetCurrentPrice()
        {
            return Task.FromResult(1.222338);
        }

        public Task<List<MtOrder>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task<List<MqlRates>> GetRates(string symbol, ENUM_TIMEFRAMES timeframes, int startpos, int count)
        {
            return Task.FromResult(CommonFunctions.DeserializeObject<List<MqlRates>>(@"C:\MqlRates\PERIOD_M1_100"));
        }

        public Task<bool> isTradingEnabled()
        {
            return Task.FromResult(TradeEnabled);
        }

        public Task<int> OpBuy(string symbol, double volume, int slippage)
        {
            return Task.FromResult(22222);
        }

        public Task<int> OpBuy(string symbol, double volume, int slippage, double stoploss, double takeprofit)
        {
            return Task.FromResult(33333);
        }

        public Task<int> OpSell(string symbol, double volume, int slippage)
        {
            return Task.FromResult(44444);
        }

        public Task<int> OpSell(string symbol, double volume, int slippage, double stoploss, double takeprofit)
        {
            return Task.FromResult(5555);
        }
    }
}
