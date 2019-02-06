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

        public TradeParameters GetTradeParameters( iTrenderMtApiService trenderMtApiService)
        {
            return new TradeParameters("EURUSD",0.1,0)
            {
                 
            };
        }
    }
}
