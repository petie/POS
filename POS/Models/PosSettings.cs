using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    namespace POS
    {
        using System;
        using System.Collections.Generic;

        using System.Globalization;
        using System.IO.Ports;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;
        using Posnet;

        public partial class Welcome
        {
            [JsonProperty("POSSettings")]
            public PosSettings PosSettings { get; set; }
        }

        public partial class PosSettings
        {
            [JsonProperty("TaxRates")]
            public List<TaxRate> TaxRates { get; set; }

            [JsonProperty("FiscalPrinter")]
            public FiscalPrinter FiscalPrinter { get; set; }
        }

        public partial class FiscalPrinter
        {
            [JsonProperty("Port")]
            public string Port { get; set; }

            [JsonProperty("Handshake")]
            public string HandshakeString { get; set; }

            public Handshake Handshake { get
                {
                    switch (HandshakeString?.ToLower())
                    {
                        case "rts":
                            return Handshake.RequestToSend;
                        case "xonxoff":
                            return Handshake.XOnXOff;
                        case "rtsxonxoff":
                            return Handshake.RequestToSendXOnXOff;
                        default:
                            return Handshake.None;
                    }
                } }

            [JsonProperty("BaudRate")]
            public int BaudRate { get; set; }

            public PosnetSettings GetPosnetSettings()
            {
                return new PosnetSettings
                {
                    BaudRate = BaudRate,
                    Handshake = Handshake,
                    Name = "TARGI",
                    Port = Port
                };
            }
        }

        public partial class TaxRate
        {
            [JsonProperty("value")]
            public int Value { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public partial class Welcome
        {
            public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, POS.Converter.Settings);
        }

        public static class Serialize
        {
            public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, POS.Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }
    }

}
