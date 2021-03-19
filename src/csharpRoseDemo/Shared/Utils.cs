using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoseSample.Constants;
using RoseSample.Identity;

namespace RoseSample.Helper
{
    public class Utils
    {
        public static void BackToMenu()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
            Console.WriteLine("Enter to back to menu!");
        }

        public static async Task<HttpResponseMessage> HttpClientRequest(object bodyEntity, string request, AuthData authData)
        {
            using (HttpClient client = new HttpClient())
            {
                await authData.authenticationProvider.SetAccessTokenAsync(client);

                // Build the request
                HttpRequestMessage InvoiceMessage;
                string resourceLocation = string.Format("{0}/api/{1}/{2}/{3}", RoseConstants.appBaseUrl, authData.AccountKey, authData.SubscriptionKey, request);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if(bodyEntity == null)
                { 
                    InvoiceMessage = new HttpRequestMessage(HttpMethod.Get, resourceLocation);
                }
                else
                {
                    InvoiceMessage = new HttpRequestMessage(HttpMethod.Post, resourceLocation);
                    string IRequest = JsonConvert.SerializeObject(bodyEntity);

                    InvoiceMessage.Content = new StringContent(IRequest, Encoding.UTF8, "application/json");
                }
                // Send

                return await client.SendAsync(InvoiceMessage);
            }
        }
    }
}
