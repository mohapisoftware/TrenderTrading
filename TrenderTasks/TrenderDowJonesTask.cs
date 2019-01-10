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

        private Boolean enableTrading=false;
        private Boolean isTaskRunning = false;
        public TrenderDowJonesTask(iTrenderMtApiService TrenderMtApiService,iTrenderDowJonesService TrenderDowJonesService)
        {
            this._TrenderMtApiService = TrenderMtApiService;
            this._TrenderDowJonesService = TrenderDowJonesService;

        }
        public async override Task<bool> ExcecuteTask(int TaskID)
        {
            enableTrading = true;
            isTaskRunning = true;
            await _TrenderMtApiService.Connect();

            while (_TrenderMtApiService.GetConnectionState().Result!= TrenderConnectionState.Failed )
            {
                if (_TrenderMtApiService.GetConnectionState().Result == TrenderConnectionState.Connected)
                {
                    TrenderTradeOperation tradeOperation = await _TrenderDowJonesService.GetTradeOperation();

                    int tradeID = 0;

                    switch (tradeOperation)
                    {
                        case TrenderTradeOperation.OpBuy:
                            tradeID = await _TrenderMtApiService.OpBuy();
                            break;
                        case TrenderTradeOperation.OpSell:
                            tradeID = await _TrenderMtApiService.OpSell();
                            break;
                        case TrenderTradeOperation.OpNeutral:
                            break;
                        default:
                            break;
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
