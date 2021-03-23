using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RoseSample.Entities
{
    /// <summary>
    /// Describes an journal entry. A journal entry is valid whem:
    /// There are two lines. One for the credit and other for debit amount.
    /// The credit amount is equal to debit amount.
    /// </summary>
    public class JournalEntryResource
    {
        /// <summary>
        /// Gets or sets the journal fiscal year.
        /// </summary>
        [JsonProperty("fiscalYear")]
        public int FiscalYear { get; set; }

        /// <summary>
        /// Gets or sets the company id where the document will be created.
        /// </summary>
        [JsonProperty("company")]
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the journal type.
        /// </summary>
        [JsonProperty("journalType")]
        public string JournalType { get; set; }

        /// <summary>
        /// Gets or sets the ledger. A ledger is a company's set of numbered accounts for its accounting records
        /// </summary>
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        /// <summary>
        /// Gets or sets the currency for the transaction.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the document lines.
        /// </summary>
        [JsonProperty("journalLines")]
        public List<JournalEntryLineResource> Lines { get; set; }


        /// <summary>
        /// Gets or sets the Party for the transaction.
        /// </summary>
        [JsonProperty("mainParty")]
        public string Party { get; set; }
    }
}
