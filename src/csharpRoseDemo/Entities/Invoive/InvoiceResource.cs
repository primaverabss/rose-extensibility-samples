using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RoseSample.Currency;

namespace RoseSample.Entities
{
    /// <summary>
    /// The invoice entity. That have only a set of properties.
    /// </summary>
    internal class InvoiceResource
    {
        #region Public Properties

        [JsonProperty("id")]
        public Guid? InvoiceId { get; set; }

        [JsonProperty("buyerCustomerParty")]
        public string Customer { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("discount")]
        public double? Discount { get; set; }

        [JsonProperty("payableAmount")]
        public Amount PayableAmount { get; set; }

        [JsonProperty("documentDate")]
        public DateTime? DocumentDate { get; set; }

        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        [JsonProperty("paymentTerm")]
        public string PaymentTerm { get; set; }

        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("deliveryTerm")]
        public string DeliveryTerm { get; set; }

        [JsonProperty("deliveryMode")]
        public string DeliveryMode { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("documentLines")]
        public List<InvoiceLineResource> Lines { get; set; }

        #endregion
    }
}
