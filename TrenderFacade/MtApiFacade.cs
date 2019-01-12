using System;
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
        private MtApiClient MtApiClient;
        private string host;
        private int port;
        private Boolean TradeEnabled = false;
        private MtConnectionState connectionState = MtConnectionState.Disconnected;

        public MtAPIFacade(string host, int port)
        {
            this.MtApiClient = new MtApiClient();
            this.host = host;
            this.port = port;

            MtApiClient.ConnectionStateChanged += MtApiClient_ConnectionStateChanged;
            MtApiClient.OnLastTimeBar += MtApiClient_OnLastTimeBar;
        }

        private void MtApiClient_OnLastTimeBar(object sender, TimeBarArgs e)
        {
            TradeEnabled = true;
        }

        private void MtApiClient_ConnectionStateChanged(object sender, MtConnectionEventArgs e)
        {
            connectionState = e.Status;
        }

        public Task<int> CloseTrade(int tradeID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Connect()
        {
            MtApiClient.BeginConnect(host, port);
            return Task.FromResult(true);
        }

        public Task<bool> Disconnect()
        {
            MtApiClient.BeginDisconnect();
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
            int orderID =  MtApiClient.OrderSendBuy(symbol, volume, slippage);

            return Task.FromResult(orderID);
        }

        public Task<int> OpSell(string symbol, double volume, int slippage)
        {
            int orderID = MtApiClient.OrderSendSell(symbol, volume, slippage);

            return Task.FromResult(orderID);
        }

        public Task<int> OpBuy(string symbol, double volume, int slippage,double stoploss,double takeprofit)
        {
            int orderID = MtApiClient.OrderSendBuy(symbol, volume, slippage, stoploss, takeprofit);

            return Task.FromResult(orderID);
        }

        public Task<int> OpSell(string symbol, double volume, int slippage, double stoploss, double takeprofit)
        {
            int orderID = MtApiClient.OrderSendSell(symbol, volume, slippage, stoploss, takeprofit);

            return Task.FromResult(orderID);
        }

        public Task<double> GetCurrentPrice()
        {
            return Task.FromResult(0.0);
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
    }
}
