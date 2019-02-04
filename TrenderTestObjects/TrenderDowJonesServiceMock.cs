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
        public async Task<TrenderTradeOperation> GetTradeOperation(iTrenderMtApiService trenderMtApiService, string symbol, ENUM_TIMEFRAMES timeframes, int startpos, int count)
        {
            List<MqlRates> rates = await trenderMtApiService.GetRates(symbol, timeframes, startpos, count);

            if (!rates.Any())
            {
                return TrenderTradeOperation.OpStayAside;
            }
            return Task.FromResult(TrenderTradeOperation.OpBuy).Result;
        }
    }
}
