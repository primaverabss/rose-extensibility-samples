using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoseSample.Entities
{
    /// <summary>
    /// The customer entity. That have only a set of properties.
    /// More information where https://apidoc.rose.primaverabss.com/
    /// </summary>
    class CustomerResource
    {
        #region Internal Properties


        [JsonProperty("CustomerGroup")]
        public string CustomerGroup { get; set; }

        [JsonProperty("PaymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("PaymentTerm")]
        public string PaymentTerm { get; set; }

        [JsonProperty("PartyTaxSchema")]
        public string PartyTaxSchema { get; set; }

        [JsonProperty("Locked")]
        public bool Locked { get; set; }

        [JsonProperty("OneTimeCustomer")]
        public bool OneTimeCustomer { get; set; }

        [JsonProperty("EndCustomer")]
        public bool EndCustomer { get; set; }

        [JsonProperty("PartyKey")]
        public string PartyKey { get; set; }

        [JsonProperty("SearchTerm")]
        public string SearchTerm { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
       
  
        #endregion
    }
}
