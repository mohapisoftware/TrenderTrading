using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trender;

namespace Trender
{
    public class TradeFacade : iTradeService
    {
        public TradeFacade()
        {

        }

        public TradeParameters GetTradeParameters(TrenderTradeOperation tradeOperation, iTrenderMtApiService trenderMtApiService)
        {
            throw new NotImplementedException();
        }
    }
}
