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
            double currentprice = trenderMtApiService.GetCurrentPrice("EURUSD").Result;
            double atr = trenderMtApiService.GetATR("EURUSD", MtApi.ENUM_TIMEFRAMES.PERIOD_M1, 14, 0).Result;

            double take
            return new TradeParameters("EURUSD",0.2,0)
            {
                 
            };
        }
    }
}
