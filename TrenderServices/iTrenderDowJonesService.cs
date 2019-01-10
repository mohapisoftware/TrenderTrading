using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public interface iTrenderDowJonesService
    {
        Task<TrenderTradeOperation> GetTradeOperation();
    }

}
