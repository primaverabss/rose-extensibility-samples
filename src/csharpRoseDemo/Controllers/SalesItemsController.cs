
using RoseSample.Entities.SalesItems;
using Newtonsoft.Json;
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
    /// Controller to handle with the sales itens actions.
    /// </summary>
    class SalesItemsController
    {
        #region POST

        public static async Task GetSalesItemsAsync(AuthData authData)
        {
            Console.WriteLine("Getting Sales Items ...");

            await RequestData("/salescore/salesItems", authData);

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
                    IList<SalesItems> orders = JsonConvert.DeserializeObject<IList<SalesItems>>(result);

                    List<Tuple<string, string, string, string, string>> lst = new List<Tuple<string, string, string, string, string>>();
                    foreach (var i in orders)
                    {
                        string lstPrices = "";
                        foreach (var price in i.PriceLines)
                        {
                            lstPrices += price.PriceList + ": " + price.PriceAmount.Value + "; ";
                        }

                        Tuple<string, string, string, string, string> item = new Tuple<string, string, string, string, string>(
                            i.Item, i.Description, i.BaseUnit, i.Barcode??"", lstPrices);
                        lst.Add(item);
                    }
                    Console.WriteLine(lst.ToStringTable(35,
                      new[] { "Item", "Description", "BaseUnit", "Barcode", "Sales Prices" },
                        a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5));

                    return result.ToString();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(string.Concat("Failed. ", responseContent.ToString()));
                    string result = await (responseContent.Content).ReadAsStringAsync();
                    Console.WriteLine(string.Concat("Content: ", result));

                    throw new Exception("Unable to create the sales items.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Error creating the sales items.");
            }
        }

        #endregion
    }
}
