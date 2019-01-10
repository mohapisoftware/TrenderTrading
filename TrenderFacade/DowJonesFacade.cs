using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public class DowJonesFacade : iTrenderDowJonesService
    {
        public Task<TrenderTradeOperation> GetTradeOperation()
        {
            throw new NotImplementedException();
        }
    }

    public class MtAPIFacade : iTrenderMtApiService
    {
        public Task<int> CloseTrade(int tradeID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Connect()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Disconnect()
        {
            throw new NotImplementedException();
        }

        public Task<TrenderConnectionState> GetConnectionState()
        {
            throw new NotImplementedException();
        }

        public Task<int> OpBuy()
        {
            throw new NotImplementedException();
        }

        public Task<int> OpSell()
        {
            throw new NotImplementedException();
        }
    }
}
