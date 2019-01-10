using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public interface iTrenderMtApiService
    {
        Task<Boolean> Connect();

        Task<int> OpBuy();

        Task<int> CloseTrade(int tradeID);
        Task<int> OpSell();

        Task<TrenderConnectionState> GetConnectionState();

        Task<Boolean> Disconnect();
    }
}
