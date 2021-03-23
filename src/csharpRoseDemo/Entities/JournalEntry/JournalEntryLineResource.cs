using Newtonsoft.Json;
using RoseSample.Currency;

namespace RoseSample.Entities
{
    /// <summary>
    /// Describes a line in an journal entry.
    /// </summary>
    public class JournalEntryLineResource
    {
        /// <summary>
        /// Gets or sets the acount for the selected general ledger
        /// </summary>
        [JsonProperty("gLAccount")]
        public string GLAccount { get; set; }

        /// <summary>
        /// Gets or sets the item description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the debit amount.
        /// </summary>
        [JsonProperty("debitAmount")]
        public Amount DebitAmount { get; set; }

        /// <summary>
        /// Gets or sets the credit amount.
        /// </summary>
        [JsonProperty("creditAmount")]
        public Amount CreditAmount { get; set; }
        /// <summary>
        /// Gets or sets the Cash Flow Item.
        /// </summary>
        [JsonProperty("cashFlowItem")]
        public string CashFlowItem { get; set; }
        /// <summary>
        /// Gets or sets the Cash Flow Item.
        /// </summary>
        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }
    }
}
