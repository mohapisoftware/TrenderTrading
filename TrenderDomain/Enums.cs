using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public enum TrenderTradeOperation
    {
        OpBuy=1
            ,OpSell=2
            ,OpNeutral=3
    }

    public enum TrenderConnectionState
    {
        Disconnected=1,
        Connecting=2,
        Failed=3,
        Connected=4
    }
}
