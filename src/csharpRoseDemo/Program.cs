using RoseSample.Controllers;
using System;
using System.Threading.Tasks;
using RoseSample.Identity;
using RoseSample.Helper;
using RoseSample.Constants;

namespace RoseSample
{
    static class Program
    {
        #region Public Methods

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            AuthData authData = new AuthData();
            authData.AccountKey = RoseConstants.AccountKey;
            authData.SubscriptionKey = RoseConstants.SubscriptionKey;
            authData.authenticationProvider = new AuthenticationProvider();

            int option;

            do
            {
                Console.Clear();
                Console.WriteLine("WELCOME TO ROSE SALES DEMO");
                Console.WriteLine("");
                Console.WriteLine("1. Get Invoices");
                Console.WriteLine("2. Create an Invoice");
                Console.WriteLine("3. Get Orders");
                Console.WriteLine("4. Create Order");
                Console.WriteLine("5. Create Customer");
                Console.WriteLine("6. Get Sales Items");
                Console.WriteLine("7. Get Journal Entries");
                Console.WriteLine("8. Create Journal Entries");
                Console.WriteLine("0. Exit");

                Console.WriteLine("");
                Console.WriteLine("What do you want to do? ");
                option = Int32.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine("Starting ...");

                    Console.Clear();
                    DoItAsync(option, authData).Wait();

                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    Utils.BackToMenu();

                    Console.ReadLine();

                }
            }
            while (option != 0);
        }

        #endregion

        #region Private Methods

        private static void exit()
        {
            Console.WriteLine();
            Console.WriteLine("Bye Bye...click enter to leave!");
        }

        private static async Task<int> DoItAsync(int option, AuthData authData)
        {
            switch (option)
            {
                case 1:
                    await InvoiceController.GetInvoicesAsync(authData);
                    break;

                case 2:
                    await InvoiceController.CreateInvoiceAsync(authData);
                    break;

                case 3:
                    await OrderController.GetOrdersAsync(authData);
                    break;

                case 4:
                    await OrderController.CreateOrderAsync(authData);
                    break;

                case 5:
                    await CustomerController.CreateCustomerAsync(authData);
                    break;

                case 6:
                    await SalesItemsController.GetSalesItemsAsync(authData);
                    break;

                case 7:
                    await JounalEntryController.GetJournalEntryAsync(authData);
                    break;

                case 8:
                    await JounalEntryController.CreateJournalEntryAsync(authData);
                    break;

                default:
                    exit();
                    break;
            }

            return 0;
        }
    }


    #endregion
}