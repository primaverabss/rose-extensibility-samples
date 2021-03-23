using RoseSample.Entities;
using RoseSample.Currency;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RoseSample.Identity;
using RoseSample.Helper;

namespace RoseSample.Controllers
{
    /// <summary>
    /// Controller to handle with the customer actions.
    /// </summary>
    class CustomerController
    {
        #region POST

        public static async Task CreateCustomerAsync(AuthData authData)
        {
            Console.WriteLine("Create ICustomer ...");

            await RequestData(CreateSampleCustomer(), "salesCore/customerParties", authData);
            Console.ForegroundColor = ConsoleColor.Gray;
            Utils.BackToMenu();
        }
        private static CustomerResource CreateSampleCustomer()
        {
            return new CustomerResource
            {
                CustomerGroup = "01",
                PaymentMethod = "NUM",
                PaymentTerm = "00",
                Locked = false,
                OneTimeCustomer = false,
                EndCustomer = false,
                PartyKey = "NewCustomer1",
                SearchTerm = "NewCustomer1",
                Name = "New Customer1"
            };
        }
        private async static Task<Guid> RequestData(object bodyEntity, string request, AuthData authData)
        {
            try
            {
                HttpResponseMessage responseContent = await Utils.HttpClientRequest(bodyEntity, request, authData);

                if (responseContent.IsSuccessStatusCode)
                {
                    string result = await responseContent.Content.ReadAsStringAsync();
                    var result1 = result.Substring(1, 36);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Concat("Customer created: ", result));

                    return (Guid.Parse(result1));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(string.Concat("Failed. ", responseContent.ToString()));
                    string result = await (responseContent.Content).ReadAsStringAsync();
                    Console.WriteLine(string.Concat("Content: ", result));

                    throw new Exception("Unable to create the Customer.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Error creating the Customer.");
            }
        }

        #endregion
    }
}
