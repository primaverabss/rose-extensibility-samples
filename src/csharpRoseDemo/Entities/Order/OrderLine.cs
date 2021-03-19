using Newtonsoft.Json;
using RoseSample.Currency;

namespace RoseSample.Entities
{
    class OrderLine
    {
        [JsonProperty("salesItem")]
        public string SalesItem { get; set; }

        [JsonProperty("unitPrice")]
        public Amount UnitPrice { get; set; }
    }
}
