﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Trender
{
    public class TrenderMtApiServiceMock : iTrenderMtApiService
    {
        private string host;
        private int port;

        public TrenderMtApiServiceMock(string host, int port)
        {
            this.host = "127.0.0.1";
            this.port = 1443;

        }
        public Task<int> CloseTrade(int tradeID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Connect()
        {
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