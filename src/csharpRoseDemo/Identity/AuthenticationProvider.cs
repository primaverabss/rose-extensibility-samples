using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using RoseSample.Constants;

namespace RoseSample.Identity
{
    /// <summary>
    /// Application autorization manager.
    /// </summary>
    public class AuthenticationProvider
    {
        #region Members

        private string accessToken;
        private DateTime tokenExpirationDate;

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the access to the currente client.
        /// </summary>
        /// <param name="client">The client that make the request.</param>
        /// <returns></returns>
        public async Task SetAccessTokenAsync(HttpClient client)
        {
            if (string.IsNullOrEmpty(this.accessToken) || this.tokenExpirationDate <= DateTime.Now)
            {
                await this.RequestAccessTokenAsync();
            }

            client.SetBearerToken(this.accessToken);
        }

        /// <summary>
        /// Request a tokem from the identity server.
        /// </summary>
        /// <returns></returns>
        internal async Task RequestAccessTokenAsync()
        {
            var client = new HttpClient();

            // Build the request.

            TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(
                   new ClientCredentialsTokenRequest
                   {
                       ClientSecret = RoseConstants.clientSecret,
                       ClientId = RoseConstants.clientId,
                       Address = RoseConstants.IdentitUriKey,
                       Scope = RoseConstants.ApplicationScopes
                   });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            this.accessToken = tokenResponse.AccessToken;

            if (string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new Exception("Failed to obtain the access token.");
            }

            this.tokenExpirationDate = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);
        }

        /// <summary>
        /// Classic method to request a tokem from the identity server.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync()
        {
            // Get the authorization token using the client credentials grant type

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Requesting the access token from the authorization server...");

                    client.BaseAddress = new Uri(RoseConstants.appBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Build the request data (grant type client credentials)

                    List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                    postData.Add(new KeyValuePair<string, string>("scope", "rose-api"));
                    postData.Add(new KeyValuePair<string, string>("client_id", RoseConstants.clientId));
                    postData.Add(new KeyValuePair<string, string>("client_secret", RoseConstants.clientSecret));

                    FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                    // Post the request and get the response

                    HttpResponseMessage response = await client.PostAsync(RoseConstants.IdentitUriKey, content);
                    string jsonString = await response.Content.ReadAsStringAsync();
                    object responseData = JsonConvert.DeserializeObject(jsonString);

                    // The access token in the response

                    return ((dynamic)responseData).access_token;

                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error getting token. {0}", ex.Message));
                }
            }
        }

        #endregion
    }
}
