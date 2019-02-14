using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MtApi;

namespace Trender
{
    public enum TrenderTradeOperation
    {
        OpBuy=1
            ,OpSell=2
            ,OpStayAside=3
    }

    public enum TrenderConnectionState
    {
        Disconnected=1,
        Connecting=2,
        Failed=3,
        Connected=4
    }

    public class TradeParameters
    {
        public string Symbol { get; set; }
        public double TakeProfit { get; set; }
        public double StopLoss { get; set; }
        public double Volume { get; set; }
        public int Slippage { get; set; }

        public ENUM_TIMEFRAMES timeframes = ENUM_TIMEFRAMES.PERIOD_M1;

        public TradeParameters(string symbol, ENUM_TIMEFRAMES timeframes)
        {
            this.Symbol = symbol;
            this.timeframes = timeframes;
        }

        public TradeParameters(string Symbol, double Volume, int Slippage)
        {
            this.Symbol = Symbol;
            this.Volume = Volume;
            this.Slippage = Slippage;
        }



        public TradeParameters(string Symbol, double Volume, int Slippage, double TakeProfit, double StopLoss)
        {
            this.Symbol = Symbol;
            this.Volume = Volume;
            this.Slippage = Slippage;
            this.TakeProfit = TakeProfit;
            this.StopLoss = StopLoss;
        }
    }

    public class CommonFunctions
    {
        public static T DeserializeObject<T>(string a_fileName)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));

            TextReader reader = new StreamReader(a_fileName);

            object obj = deserializer.Deserialize(reader);

            reader.Close();

            return (T)obj;
        }

        public static void SerializeObject<T>(object mqlRates, string a_fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (var stream = File.OpenWrite(a_fileName))
            {
                serializer.Serialize(stream, mqlRates);
            }
        }
    }
}
