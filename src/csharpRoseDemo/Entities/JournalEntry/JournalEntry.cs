using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RoseSample.Currency;

namespace RoseSample.Entities
{
    /// <summary>
    /// The invoice entity. Have only a set of properties.
    /// More information where https://apidoc.rose.primaverabss.com/
    /// </summary>
    internal class JournalEntry
    {
        #region Public Properties

        [JsonProperty("naturalKey")]
        public string NaturalKey
        {
            get;
            set;
        }

        [JsonProperty("totalDebit")]
        public Amount TotalDebit
        {
            get;
            set;
        }

        [JsonProperty("totalCredit")]
        public Amount TotalCredit
        {
            get;
            set;
        }

        [JsonProperty("company")]
        public string Company
        {
            get;
            set;
        }

        [JsonProperty("postingDate")]
        public string PostingDate
        {
            get;
            set;
        }

        #endregion
    }
}
