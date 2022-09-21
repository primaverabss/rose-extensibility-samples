using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeviceCodeFlow
{
    class Program
    {
        const string AUTHORITYSERVER_BASEADDRESS = "https://stg-identity.primaverabss.com";
        const string PRODUCT_BASEADDRESS = "https://st-app.rose.primaverabss.com";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting ...");

                DoIt().Wait();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// The main task.
        /// </summary>
        /// <returns></returns>
        private static async Task DoIt()
        {
            HttpClient client = new HttpClient();

            var deviceAuthorizationResponse = await RequestDeviceAuthorizationAsync(client);

            var acessTokenResponse = await RequestAccessTokenAsync(client, deviceAuthorizationResponse);

            await RetrieveSubscriptionsAsync(client, acessTokenResponse);
        }

        /// <summary>
        /// Start the client authorization process.
        /// </summary>
        /// <param name="client">The client that request the information.</param>
        /// <returns>The authorization response</returns>
        private static async Task<DeviceAuthorizationResponse> RequestDeviceAuthorizationAsync(HttpClient client)
        {
            Console.WriteLine("Requesting authorization...");

            Uri authorizationEndpointUri = new Uri(AUTHORITYSERVER_BASEADDRESS);
            Uri deviceAuthorizationEndpointUri = new Uri(authorizationEndpointUri, "/connect/deviceauthorization");

            var request = new HttpRequestMessage(HttpMethod.Post, deviceAuthorizationEndpointUri)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    //The clientID defined at the Appstore portal.
                    ["client_id"] = "saft-importer-devicecode"
                })
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var authorizationResponse = JsonSerializer.Deserialize<DeviceAuthorizationResponse>(json);

            Process.Start(
                new ProcessStartInfo(authorizationResponse.VerificationUriComplete)
                {
                    UseShellExecute = true
                });

            return authorizationResponse;
        }

        /// <summary>
        /// Request the access token.
        /// </summary>
        /// <param name="client">The client that request the information.</param>
        /// <param name="authResponse">The authorization code.</param>
        /// <returns>The access token</returns>
        private static async Task<TokenResponse> RequestAccessTokenAsync(HttpClient client, DeviceAuthorizationResponse authResponse)
        {
            Console.WriteLine("Requesting access token...");

            Uri authorizationEndpointUri = new Uri(AUTHORITYSERVER_BASEADDRESS);
            Uri tokenEndpointUri = new Uri(authorizationEndpointUri, "/connect/token");

            int pollingDelay = authResponse.Interval;

            while (true)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpointUri)
                {
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        ["grant_type"] = "urn:ietf:params:oauth:grant-type:device_code",
                        ["device_code"] = authResponse.DeviceCode,
                        ["client_id"] = "saft-importer-devicecode"
                    })
                };

                var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<TokenResponse>(json);
                }
                else
                {
                    var responseMsg = JsonSerializer.Deserialize<TokenErrorResponse>(json);

                    if (responseMsg.Error.Equals("authorization_pending") || responseMsg.Error.Equals("slow_down"))
                    {
                        Console.WriteLine($"{responseMsg.Error}. Waiting...");
                        await Task.Delay(TimeSpan.FromSeconds(pollingDelay));
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"Token request failed with error: {responseMsg.Error}.");
                    }
                }
            }
        }

        /// <summary>
        /// Get the subscription information for the currente user.
        /// </summary>
        /// <param name="client">The client that request the information.</param>
        /// <param name="tokenInfo">The Tokem.</param>
        /// <returns></returns>
        private static async Task RetrieveSubscriptionsAsync(HttpClient client , TokenResponse tokenInfo)
        {
            Console.WriteLine("Invoking the API...");

            Uri productEndpointUri = new Uri(PRODUCT_BASEADDRESS);
            Uri subscriptionEndpointUri = new Uri(productEndpointUri, "/api/cmsSubscriptions/getSubscriptions");

            HttpRequestMessage httpRequest = new HttpRequestMessage( HttpMethod.Get,subscriptionEndpointUri);

            httpRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tokenInfo.AccessToken}");

            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"API request failed with error: {httpResponse.ReasonPhrase}.");
            }

            string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            Console.WriteLine("Success!");
        }
    }
}
