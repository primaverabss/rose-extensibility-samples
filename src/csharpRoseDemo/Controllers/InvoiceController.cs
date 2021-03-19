
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RoseSample.Entities;
using RoseSample.Currency;
using RoseSample.Identity;
using RoseSample.Helper;

namespace RoseSample.Controllers
{
    /// <summary>
    /// Controller to handle with the invoice actions.
    /// </summary>
    class InvoiceController
    {
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
    }
}
