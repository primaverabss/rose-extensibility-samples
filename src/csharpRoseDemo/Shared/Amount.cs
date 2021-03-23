using Newtonsoft.Json;

namespace RoseSample.Currency
{
    /// <summary>
    /// Describes the amount.
    /// </summary>
    public class Amount
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("amount")]
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the currency simbol.
        /// </summary>
        [JsonProperty("symbol")]
        public string Currency { get; set; }

        public Amount()
        {
        }

        ////public Amount(double value)
        ////{
        ////    this.Value = value;
        ////}

        ////public Amount(double value, string currency)
        ////{
        ////    this.Value = value;
        ////    this.Currency = currency;
        ////}
    }
}