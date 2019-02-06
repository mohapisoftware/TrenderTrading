using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trender;

namespace Trender
{
    public interface iTradeService
    {
        TradeParameters GetTradeParameters(iTrenderMtApiService trenderMtApiService);
    }
}
