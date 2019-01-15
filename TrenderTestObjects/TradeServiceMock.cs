using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public class TradeServiceMock : iTradeService
    {
        public TradeParameters GetTradeParameters(TrenderTradeOperation tradeOperation, iTrenderMtApiService trenderMtApiService)
        {
            return new TradeParameters("EURUSD", 1.1, 0);
        }
    }
}
