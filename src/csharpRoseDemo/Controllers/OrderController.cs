
using RoseSample.Entities;
using Newtonsoft.Json;
using RoseSample.Currency;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RoseSample;
using RoseSample.Identity;
using RoseSample.Helper;

namespace RoseSample.Controllers
{
    /// <summary>
    /// Controller to handle with the order actions.
    /// </summary>
    class OrderController
    {
        public static async Task GetOrdersAsync(AuthData authData)
        {
            Console.WriteLine("Getting Orders ...");

            await RequestData("/sales/orders?page=1&pageSize=10", authData);

            Utils.BackToMenu();
        }

        private async static Task<String> RequestData(string request, AuthData authData )
        {
            try
            {
                HttpResponseMessage responseContent = await Utils.HttpClientRequest(null, request, authData);

                if (responseContent.IsSuccessStatusCode)
                {
                    var result = await responseContent.Content.ReadAsStringAsync();

                    dynamic oDataResponse = JsonConvert.DeserializeObject(result);

                    List<Order> orders = oDataResponse.data.ToObject<List<Order>>();
                    

                    List<Tuple<string, string, string, string>> lst = new List<Tuple<string, string, string, string>>();
                    foreach (var i in orders)
                    {
                        Tuple<string, string, string, string> item = new Tuple<string, string, string, string>(i.PostingDate, i.NaturalKey, i.BuyerCustomerPartyDescription, i.PayableAmount.Value.ToString());
                        lst.Add(item);
                    }
                    Console.WriteLine(lst.ToStringTable(
                      new[] { "Date", "Order", "Customer", "Total" },
                      a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4));

                    return result.ToString();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(string.Concat("Failed. ", responseContent.ToString()));
                    string result = await (responseContent.Content).ReadAsStringAsync();
                    Console.WriteLine(string.Concat("Content: ", result));

                    throw new Exception("Unable to create the order.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Error creating the order.");
            }
        }

        public static async Task CreateOrderAsync(AuthData authData )
        {
            #region Build the budget that should be created

            Order order = new Order();

            order.BuyerCustomerParty = "SOFRIO";
            order.Company = "Default";
            order.DocumentType = "ECL";
            order.PaymentMethod = "NUM";
            order.PaymentTerm = "00";

            order.Lines = new List<OrderLine>();

            //IEnumerable<BudgetGLAccount> generatedLines = await GetGLAccountsForBudgetGroupAsync(Company, BudgetGroup);

            OrderLine newLine = new OrderLine
            {
                SalesItem = "0001",
                UnitPrice = new Amount()
                {
                    Value = 40
                }
            };

            order.Lines.Add(newLine);

            #endregion

            try
            {
                HttpResponseMessage responseContent = await Utils.HttpClientRequest(order, "sales/orders", authData);

                if (responseContent.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    string result = await (responseContent.Content).ReadAsStringAsync();
                    Console.WriteLine(string.Concat("Order created: ", result));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(string.Concat("Failed. ", responseContent.ToString()));
                    string result = await (responseContent.Content).ReadAsStringAsync();
                    Console.WriteLine(string.Concat("Content: ", result));
                }
            }
            catch (Exception)
            {
                //throw new Exception("Error creating the budget.");

                Console.WriteLine("Unable to create the order.");
            }
            finally
            {
                Utils.BackToMenu();
            }
        }
    }
}
