﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtApi;

namespace Trender
{
    public abstract class TrenderDowJonesBaseTask : iTrenderTask
    {
        public virtual Task<bool> ExcecuteTask(int TaskID)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsTaskRunning()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> RemoveTask(int TaskID)
        {
            throw new NotImplementedException();
        }
    }
    public class TrenderDowJonesTask : TrenderDowJonesBaseTask
    {
        private iTrenderDowJonesService _TrenderDowJonesService;
        private iTrenderMtApiService _TrenderMtApiService;
        private iTradeService  _TradeService;

        private Boolean enableTrading=false;
        private Boolean isTaskRunning = false;

        //get from database
        string symbol = "EURUSD";
        ENUM_TIMEFRAMES timeframes = ENUM_TIMEFRAMES.PERIOD_M1;
        int startpos=0;
        int count=10;
        int mashift = 0;
        int mamethod = 1;
        int shift = 1;
        int period = 14;
        int appliedprice = 1;

        public TrenderDowJonesTask(iTrenderMtApiService TrenderMtApiService,iTrenderDowJonesService TrenderDowJonesService, iTradeService TradeService)
        {
            this._TrenderMtApiService = TrenderMtApiService;
            this._TrenderDowJonesService = TrenderDowJonesService;
            this._TradeService = TradeService;
        }
        public async override Task<bool> ExcecuteTask(int TaskID)
        {
            //get parameters for task

            enableTrading = true;
            isTaskRunning = true;
            await _TrenderMtApiService.Connect();
            
            while (_TrenderMtApiService.GetConnectionState().Result!= TrenderConnectionState.Failed )
            {
                int tradeID = 0;
                if (_TrenderMtApiService.GetConnectionState().Result == TrenderConnectionState.Connected)
                {
                    if (_TrenderMtApiService.isTradingEnabled().Result)
                    {
                        var datestring = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();

                        TradeParameters tradeParameters = _TradeService.GetTradeParameters(_TrenderMtApiService);
                        //close qualifying orders
                        //CloseOrders(tradeParameters);

                        TrenderTradeOperation tradeOperation = await _TrenderDowJonesService.GetTradeOperation(_TrenderMtApiService,symbol,timeframes,startpos,count);

                        double atr = _TrenderMtApiService.GetATR(symbol, timeframes, 14, 0).Result;
                        double currentprice = _TrenderMtApiService.GetCurrentPrice(symbol).Result;
                        double ma = _TrenderMtApiService.GetMA(symbol, timeframes, period, mashift, mamethod, appliedprice, shift).Result;
                        double takeprofit = 0.0;
                        double stoploss = 0.0;
                        TrenderTradeOperation maSentiment = TrenderTradeOperation.OpStayAside;

                        if (ma< currentprice)
                        {
                            maSentiment = TrenderTradeOperation.OpBuy;
                        }
                        else
                        {
                            maSentiment = TrenderTradeOperation.OpSell;
                        }
                        if (tradeOperation!= maSentiment)
                        {
                            Console.WriteLine(datestring+" ,cannot trade against the MA/trend.");
                            await _TrenderMtApiService.DisableTrading();
                            continue;
                        }
                        //calculate stop loss and take profit
                        if (tradeOperation == TrenderTradeOperation.OpBuy)
                        {
                            takeprofit = currentprice + atr*2 ;
                            stoploss = currentprice - atr;
                        }
                        else
                        {
                            takeprofit = currentprice - atr*2 ;
                            stoploss = currentprice + atr;
                        }

                        switch (tradeOperation)
                        {
                            case TrenderTradeOperation.OpBuy:
                                var buy = _TrenderMtApiService.GetOrders().Result?.FirstOrDefault(s => s.Operation == TradeOperation.OP_BUY);
                                if (buy == null)
                                {
                                    tradeID = await _TrenderMtApiService.OpBuy(tradeParameters.Symbol, tradeParameters.Volume, tradeParameters.Slippage, stoploss, takeprofit);
                                }
                                else
                                {
                                    Console.WriteLine(datestring + " : cannot open more than 1 buy trade");
                                }
                                
                                break;
                            case TrenderTradeOperation.OpSell:
                                var sell = _TrenderMtApiService.GetOrders().Result?.FirstOrDefault(s => s.Operation == TradeOperation.OP_SELL);
                                if (sell == null)
                                {
                                    tradeID = await _TrenderMtApiService.OpSell(tradeParameters.Symbol, tradeParameters.Volume, tradeParameters.Slippage, stoploss, takeprofit);
                                }
                                else
                                {
                                    Console.WriteLine(datestring + " : cannot open more than 1 sell trade");
                                }
                                break;
                            case TrenderTradeOperation.OpStayAside:
                                
                                Console.WriteLine(datestring + " : No Trade:{0}", 0);
                                break;
                            default:
                                break;

                        }
                       await  _TrenderMtApiService.DisableTrading();
                    }
                }

                if (!enableTrading)
                {
                    isTaskRunning = false;
                    return true;
                }
            }
            isTaskRunning = false;
            return false;
        }

        public async override Task<bool> RemoveTask(int TaskID)
        {
            await _TrenderMtApiService.Disconnect();

            enableTrading = false;
            isTaskRunning = false;
            return false;
        }

        public override bool IsTaskRunning()
        {
            return isTaskRunning;
        }

        private async void CloseOrders(TradeParameters tradeparameters)
        {
            var datestring = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            List<MtOrder> orders = await _TrenderMtApiService.GetOrders();

            foreach (var order in orders)
            {
                double profit = order.Profit;
                double commision = order.Commission;

                //if order is loosing close it
                if (profit<0)
                {
                    Console.WriteLine(datestring+",OrderID:{0} closed on loss of :{1}", order.Ticket, profit);
                    await _TrenderMtApiService.CloseTrade(order.Ticket, tradeparameters.Slippage);
                }

                //if order is making profit after commision is deducted close it
                if ((profit+commision)>0)
                {
                    Console.WriteLine(datestring + ",OrderID:{0} closed on gross profit of :{1},Net profit of {2}", order.Ticket, profit, profit+commision);
                    await _TrenderMtApiService.CloseTrade(order.Ticket, tradeparameters.Slippage);
                }
            }
        }
    }
}
