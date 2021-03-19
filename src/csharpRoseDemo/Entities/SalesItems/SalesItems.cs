using Newtonsoft.Json;
using System.Collections.Generic;

namespace RoseSample.Entities.SalesItems
{
    /// <summary>
    /// The sales item entity. That have only a set of properties.
    /// More information where https://apidoc.rose.primaverabss.com/
    /// </summary>
    class SalesItems
    {
        [JsonProperty("itemKey")] 
        public string Item { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("baseUnit")]
        public string BaseUnit { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("priceListLines")]
        public List<PriceListLine> PriceLines { get; set; }
    }
}
