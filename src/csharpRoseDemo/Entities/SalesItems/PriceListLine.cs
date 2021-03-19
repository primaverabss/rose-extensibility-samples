using Newtonsoft.Json;
using RoseSample.Currency;

namespace RoseSample.Entities.SalesItems
{
    /// <summary>
    /// The price list entity. That have only a set of properties.
    /// More information where https://apidoc.rose.primaverabss.com/
    /// </summary>
    class PriceListLine
    {
        [JsonProperty("priceAmount")]
        public Amount PriceAmount { get; set; }

        [JsonProperty("priceList")]
        public string PriceList { get; set; }
    }
}
