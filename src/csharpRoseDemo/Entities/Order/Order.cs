using Newtonsoft.Json;
using RoseSample.Currency;
using System.Collections.Generic;

namespace RoseSample.Entities
{
    /// <summary>
    /// The order entity. Have only a set of properties.
    /// More information where https://apidoc.rose.primaverabss.com/
    /// </summary>
    class Order
    {
        [JsonProperty("company")]
        public string Company { get; set; }
        
        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("paymentTerm")]
        public string PaymentTerm { get; set; }

        [JsonProperty("postingDate")]
        public string PostingDate { get; set; }

        [JsonProperty("naturalKey")]
        public string NaturalKey { get; set; }

        [JsonProperty("buyerCustomerParty")]
        public string BuyerCustomerParty { get; set; }

        [JsonProperty("buyerCustomerPartyDescription")]
        public string BuyerCustomerPartyDescription { get; set; }

        [JsonProperty("payableAmount")]
        public Amount PayableAmount { get; set; }

        [JsonProperty("documentLines")]
        public List<OrderLine> Lines { get; set; }
    }    
}
