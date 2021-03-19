
namespace RoseSample.Identity
{
    public class AuthData
    {
        /// <summary>
        /// The account key.
        /// </summary>
        public string AccountKey { get; set; }

        /// <summary>
        /// The Subscription Key.
        /// </summary>
        public string SubscriptionKey { get; set; }

        /// <summary>
        /// The Authentication Provider. Provides the token.
        /// </summary>
        public AuthenticationProvider authenticationProvider { get; set; }
    }
}