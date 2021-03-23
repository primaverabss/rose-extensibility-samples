
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RoseSample.Entities;
using RoseSample.Currency;
using RoseSample.Identity;
using RoseSample.Helper;
using Newtonsoft.Json;

namespace RoseSample.Controllers
{
    /// <summary>
    /// Controller to handle with the invoice actions.
    /// </summary>
    class InvoiceController
    {
        #region POST

        public static async Task CreateInvoiceAsync(AuthData authData)
        {
            Console.WriteLine("Create Invoice ...");

            await RequestData(CreateSampleInvoice(), "billing/invoices", authData);
            Console.ForegroundColor = ConsoleColor.Gray;
            Utils.BackToMenu();
        }
        private static InvoiceResource CreateSampleInvoice()
        {
            return new InvoiceResource
            {
                Company = "Default",
                Customer = "SOFRIO",
                DeliveryMode = "CORREIO",
                Lines = new List<InvoiceLineResource>
                    {
                        new InvoiceLineResource
                        {
                            Item = "0005",
                            Quantity = 2,
                            Description = "Item 0004 custom description.",
                            Price = new Amount { Value = 50 }
                        }
                    }
            };
        }
        private async static Task<Guid> RequestData(object bodyEntity, string request, AuthData authData)
        {
            try
            {
                HttpResponseMessage responseContent = await Utils.HttpClientRequest(bodyEntity, request, authData );

                if (responseContent.IsSuccessStatusCode)
                {
                    string result = await responseContent.Content.ReadAsStringAsync();
                    var result1 = result.Substring(1, 36);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Concat("Invoice created: ", result));

                    return (Guid.Parse(result1));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(string.Concat("Failed. ", responseContent.ToString()));
                    string result = await ((StreamContent)responseContent.Content).ReadAsStringAsync();
                    Console.WriteLine(string.Concat("Content: ", result));

                    throw new Exception("Unable to create the invoice.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());

                throw new Exception("Error creating the invoice.");
            }
        }

        #endregion

        #region GET

        public static async Task GetInvoicesAsync(AuthData authData)
        {
            Console.WriteLine("Getting Invoices ...");

            await RequestData("billing/invoices?page=1&pageSize=10", authData);

            Console.ForegroundColor = ConsoleColor.Gray;
        }

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
                HttpResponseMessage responseContent = await Utils.HttpClientRequest(null, request, authData);

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
