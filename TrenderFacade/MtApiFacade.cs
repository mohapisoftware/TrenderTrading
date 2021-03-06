﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtApi;
using Trender;

namespace Trender
{
    public class MtAPIFacade : iTrenderMtApiService
    {
        private MtApiClient _MtApiClient;
        private string host;
        private int port;
        private Boolean TradeEnabled = false;
        private MtConnectionState connectionState = MtConnectionState.Disconnected;

        public MtAPIFacade(string host, int port)
        {
            this._MtApiClient = new MtApiClient();
            this.host = host;
            this.port = port;

            _MtApiClient.ConnectionStateChanged += MtApiClient_ConnectionStateChanged;
            _MtApiClient.OnLastTimeBar += MtApiClient_OnLastTimeBar;
        }

        private void MtApiClient_OnLastTimeBar(object sender, TimeBarArgs e)
        {
            TradeEnabled = true;
        }

        private void MtApiClient_ConnectionStateChanged(object sender, MtConnectionEventArgs e)
        {
            connectionState = e.Status;
        }

        public Task<Boolean> CloseTrade(int tradeID,int slippage)
        {
            return Task.FromResult(_MtApiClient.OrderClose(tradeID, slippage));
        }

        public Task<bool> Connect()
        {
            _MtApiClient.BeginConnect(host, port);
            return Task.FromResult(true);
        }

        public Task<bool> Disconnect()
        {
            _MtApiClient.BeginDisconnect();
            return Task.FromResult(true);
        }

        public Task<TrenderConnectionState> GetConnectionState()
        {
            TrenderConnectionState connState = TrenderConnectionState.Disconnected;
            switch (connectionState)
            {
                case MtConnectionState.Disconnected:
                    connState = TrenderConnectionState.Disconnected;
                    break;
                case MtConnectionState.Connecting:
                    connState = TrenderConnectionState.Connecting;
                    break;
                case MtConnectionState.Connected:
                    connState = TrenderConnectionState.Connected;
                    break;
                case MtConnectionState.Failed:
                    connState = TrenderConnectionState.Failed;
                    break;
                default:
                    break;
            }

            return Task.FromResult(connState);
        }

        public Task<int> OpBuy(string symbol, double volume, int slippage)
        {
            
            int orderID =  _MtApiClient.OrderSendBuy(symbol, volume, slippage);
            var datestring = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            Console.WriteLine(datestring + " : OpBuy:{0}",orderID);
            return Task.FromResult(orderID);
        }

        public Task<int> OpSell(string symbol, double volume, int slippage)
        {
            int orderID = _MtApiClient.OrderSendSell(symbol, volume, slippage);
            var datestring = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            Console.WriteLine(datestring + " : OpSell:{0}", orderID);
            return Task.FromResult(orderID);
        }

        public Task<int> OpBuy(string symbol, double volume, int slippage,double stoploss,double takeprofit)
        {
            int orderID = _MtApiClient.OrderSendBuy(symbol, volume, slippage, stoploss, takeprofit);

            return Task.FromResult(orderID);
        }

        public Task<int> OpSell(string symbol, double volume, int slippage, double stoploss, double takeprofit)
        {
            int orderID = _MtApiClient.OrderSendSell(symbol, volume, slippage, stoploss, takeprofit);

            return Task.FromResult(orderID);
        }

        public Task<double> GetCurrentPrice(string symbol)
        {
            return Task.FromResult(_MtApiClient.MarketInfo(symbol, MarketInfoModeType.MODE_BID));
        }

        public Task<bool> isTradingEnabled()
        {
            return Task.FromResult(TradeEnabled);
        }

        public Task<bool> DisableTrading()
        {
            TradeEnabled = false;
            return Task.FromResult(true);
        }

        public Task<List<MtOrder>> GetOrders()
        {
            List<MtOrder> orders = _MtApiClient.GetOrders(OrderSelectSource.MODE_TRADES);
            return Task.FromResult(orders);
        }

        public Task<List<MqlRates>> GetRates(string symbol, ENUM_TIMEFRAMES timeframes, int startpos, int count)
        {
            return Task.FromResult(_MtApiClient.CopyRates(symbol,timeframes,startpos,count));
        }

        public Task<double> GetATR(string symbol, ENUM_TIMEFRAMES timeframes,int period,int shift)
        {
            return Task.FromResult(_MtApiClient.iATR(symbol, (int)timeframes,period,shift));
        }

        public Task<double> GetMA(string symbol, ENUM_TIMEFRAMES timeframes, int period, int mashift,int mamethod,int appliedprice,int shift)
        {
            return Task.FromResult(_MtApiClient.iMA(symbol, (int)timeframes, period, mashift,mamethod,appliedprice,shift));
        }
    }
}
