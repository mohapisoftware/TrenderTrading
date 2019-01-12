using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public   class TrenderDowJonesServiceMock : iTrenderDowJonesService
    {
        public Task<TrenderTradeOperation> GetTradeOperation()
        {
            return Task.FromResult(TrenderTradeOperation.OpBuy);
        }
    }
}
