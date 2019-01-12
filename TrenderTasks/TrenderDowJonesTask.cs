using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TrenderDowJonesTask(iTrenderMtApiService TrenderMtApiService,iTrenderDowJonesService TrenderDowJonesService, iTradeService TradeService)
        {
            this._TrenderMtApiService = TrenderMtApiService;
            this._TrenderDowJonesService = TrenderDowJonesService;
            this._TradeService = TradeService;
        }
        public async override Task<bool> ExcecuteTask(int TaskID)
        {
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
                        TrenderTradeOperation tradeOperation = await _TrenderDowJonesService.GetTradeOperation();
                        TradeParameters tradeParameters = _TradeService.GetTradeParameters(tradeOperation, _TrenderMtApiService.GetCurrentPrice().Result);


                        switch (tradeOperation)
                        {
                            case TrenderTradeOperation.OpBuy:
                                tradeID = await _TrenderMtApiService.OpBuy(tradeParameters.Symbol, tradeParameters.Volume, tradeParameters.Slippage);
                                break;
                            case TrenderTradeOperation.OpSell:
                                tradeID = await _TrenderMtApiService.OpSell(tradeParameters.Symbol, tradeParameters.Volume, tradeParameters.Slippage, tradeParameters.StopLoss, tradeParameters.TakeProfit);
                                break;
                            case TrenderTradeOperation.OpNeutral:
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
    }
}
