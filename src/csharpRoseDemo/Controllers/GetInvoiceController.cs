using RoseSample.Currency;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoseSample.Entities;
using RoseSample.Identity;
using RoseSample.Helper;

namespace RoseSample.Controllers
{
    /// <summary>
    /// Controller to handle with the invoice actions.
    /// </summary>
    class GetInvoiceController
    {
        public static async Task GetInvoicesAsync(AuthData authData)
        {
            Console.WriteLine("Getting Invoices ...");

            await RequestData("billing/invoices?page=1&pageSize=10", authData);

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        #region Private Methods
        /// <summary>
        /// HTTPs the client request.
        /// </summary>
        /// <param name="bodyEntity">The body entity.</param>
        /// <param name="request">The request.</param>
        /// <returns>The uniqueidentifer</returns>
        /// <exception cref="System.Exception">
        /// Unable to create the journal entry.
        /// or
        /// Error creating the journal entry.
        /// </exception>
        private async static Task<String> RequestData(string request, AuthData authData)
        {
            try
            {
                HttpResponseMessage responseContent = await Utils.HttpClientRequest(null,request, authData);

                if (responseContent.IsSuccessStatusCode)
                {
                    var result = await responseContent.Content.ReadAsStringAsync();

                    dynamic oDataResponse = JsonConvert.DeserializeObject(result);

                    List<Invoice> invoices = oDataResponse.data.ToObject<List<Invoice>>();

                    List<(string, string, string, string)> lst = new List<(string, string, string, string)>();

                    foreach (var i in invoices)
                    {
                        lst.Add((i.DueDate, i.NaturalKey, i.AccountingPartyName, i.Company));
                    }

                    Console.WriteLine(lst.ToStringTable(
                      new[] { "Invoice", "Date", "Customer", "Company" },
                      a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4));
                    
                    return result.ToString();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(string.Concat("Failed. ", responseContent.ToString()));
                    string result = await (responseContent.Content).ReadAsStringAsync();
                    Console.WriteLine(string.Concat("Content: ", result));

                    throw new Exception("Unable to create the invoice.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Error creating the invoice.");
            }
        }

        #endregion
    }
}

