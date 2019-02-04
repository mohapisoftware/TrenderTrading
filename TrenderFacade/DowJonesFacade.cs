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
        int candle0index = 1;
        int candle1index = 2;
        public async Task<TrenderTradeOperation> GetTradeOperation(iTrenderMtApiService trenderMtApiService, string symbol, ENUM_TIMEFRAMES timeframes, int startpos, int count)
        {
            List<MqlRates> rates = await trenderMtApiService.GetRates(symbol, timeframes, startpos, count);

            if (!rates.Any())
            {
                return  TrenderTradeOperation.OpStayAside;
            }
            return Task.FromResult(Calculate(rates)).Result;
        }
        private TrenderTradeOperation Calculate(List<MqlRates> rates)
        {
            //cnadles must have the same direction
            TradeOperation candle0 = TradeOperation.OP_BUYLIMIT;
            TradeOperation candle1 = TradeOperation.OP_BUYLIMIT;
            if (rates[candle0index].Close > rates[candle0index].Open)
            {
                candle0 = TradeOperation.OP_BUY;
            }
            else
            {
                candle0 = TradeOperation.OP_SELL;
            }

            if (rates[candle1index].Close > rates[candle1index].Open)
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
                Console.WriteLine("Dow Jones: Candle directions differ.");
                return TrenderTradeOperation.OpStayAside;
            }

            //high high, high glow
            if (candle0== TradeOperation.OP_BUY)
            {
                if ((rates[candle0index].High> rates[candle1index].High)&&(rates[candle0index].Low >= rates[candle1index].Low))
                {
                    return TrenderTradeOperation.OpBuy;
                }
                else
                {
                    Console.WriteLine("Dow Jones: OP_BUY highhighhighlow condition not met");
                    return TrenderTradeOperation.OpStayAside;
                }
            }

            //lower lows , lower high
            if (candle0 == TradeOperation.OP_SELL)
            {
                if ((rates[candle0index].High< rates[candle1index].High)&&((rates[candle0index].Low < rates[candle1index].Low)))
                {
                    return TrenderTradeOperation.OpSell;
                }
                else
                {
                    Console.WriteLine("Dow Jones: OP_BUY lowlowlowhigh condition not met");
                    return TrenderTradeOperation.OpStayAside;
                }
            }

            return TrenderTradeOperation.OpStayAside;
        }
    }

    
}
