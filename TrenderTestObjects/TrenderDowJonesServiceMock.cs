using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtApi;

namespace Trender
{
    public   class TrenderDowJonesServiceMock : iTrenderDowJonesService
    {
        public async Task<TrenderTradeOperation> GetTradeOperation(iTrenderMtApiService trenderMtApiService)
        {
            List<MqlRates> rates = await trenderMtApiService.GetRates();

            if (!rates.Any())
            {
                return TrenderTradeOperation.OpStayAside;
            }
            return Task.FromResult(TrenderTradeOperation.OpBuy).Result;
        }
    }
}
