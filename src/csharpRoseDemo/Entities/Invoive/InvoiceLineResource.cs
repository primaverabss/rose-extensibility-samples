using Newtonsoft.Json;
using RoseSample.Currency;

namespace RoseSample.Entities
{
    /// <summary>
    /// The invoice line entity. That have only a set of properties.
    /// </summary>
    internal class InvoiceLineResource
    {
        #region Public Properties

        [JsonProperty("salesItem")]
        public string Item { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public double? Quantity { get; set; }

        [JsonProperty("unitPrice")]
        public Amount Price { get; set; }

        #endregion
    }
}
