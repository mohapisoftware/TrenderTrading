using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtApi;

namespace Trender
{
    public class DowJonesFacade : iTrenderDowJonesService
    {

        public async Task<TrenderTradeOperation> GetTradeOperation(iTrenderMtApiService trenderMtApiService, string symbol, ENUM_TIMEFRAMES timeframes, int startpos, int count)
        {
            List<MqlRates> rates = await trenderMtApiService.GetRates(symbol, timeframes, startpos, count);

            if (!rates.Any())
            {
                return Calculate(rates);
            }
            return Task.FromResult(TrenderTradeOperation.OpBuy).Result;
        }
        private TrenderTradeOperation Calculate(List<MqlRates> rates)
        {
            //cnadles must have the same direction
            TradeOperation candle0 = TradeOperation.OP_BUYLIMIT;
            TradeOperation candle1 = TradeOperation.OP_BUYLIMIT;
            if (rates[0].Open>= rates[0].Close)
            {
                candle0 = TradeOperation.OP_BUY;
            }
            else
            {
                candle0 = TradeOperation.OP_SELL;
            }

            if (rates[1].Open >= rates[1].Close)
            {
                candle1 = TradeOperation.OP_BUY;
            }
            else
            {
                candle1 = TradeOperation.OP_SELL;
            }

            // no trade if candles 
            if (candle0!= candle1)
            {
                return TrenderTradeOperation.OpStayAside;
            }

            //high high, high glow
            if (candle0== TradeOperation.OP_BUY)
            {
                if ((rates[0].High>= rates[1].High)&&(rates[0].Low >= rates[1].Low))
                {
                    return TrenderTradeOperation.OpBuy;
                }
                else
                {
                    return TrenderTradeOperation.OpStayAside;
                }
            }

            //lower lows , lower high
            if (candle0 == TradeOperation.OP_SELL)
            {
                if ((rates[0].High< rates[1].High)&&((rates[0].Low < rates[1].Low)))
                {
                    return TrenderTradeOperation.OpSell;
                }
                else
                {
                    return TrenderTradeOperation.OpStayAside;
                }
            }

            return TrenderTradeOperation.OpStayAside;
        }
    }

    
}
