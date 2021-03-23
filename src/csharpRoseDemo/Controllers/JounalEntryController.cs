using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RoseSample.Helper;
using RoseSample.Identity;
using RoseSample.Currency;
using Newtonsoft.Json;
using RoseSample.Entities;
using System.Globalization;

namespace RoseSample.Controllers
{
    class JounalEntryController
    {
        #region POST

        public static async Task CreateJournalEntryAsync(AuthData authData)
        {
            Console.WriteLine("Create Journal Entry ...");

            await RequestData(CreateJournalEntry(), "accounting/journalEntries", authData);

            Utils.BackToMenu();
        }
        
        /// <summary>
        /// Creates a new journal entry.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/> object that represents the asynchronous task.
        /// </returns>
        private static JournalEntryResource CreateJournalEntry()
        {
            #region Build the journal entry that should be created

            JournalEntryResource journalEntry = new JournalEntryResource();

            journalEntry.Company = "Default";
            journalEntry.Ledger = "00";
            journalEntry.JournalType = "30";
            journalEntry.Currency = "EUR";
            journalEntry.Party = "SOFRIO";

            journalEntry.Lines = new List<JournalEntryLineResource>
            {
                new JournalEntryLineResource
                {
                    GLAccount = "6311",
                    Description = "Ordenado",
                    DebitAmount = new Amount()
                    {
                        Value = 2000,
                        Currency = "€"
                    }

                },

                new JournalEntryLineResource
                {
                    GLAccount = "6315",
                    Description = "Subsidio de Refeição",
                    DebitAmount = new Amount()
                    {
                        Value = 100,
                        Currency = "€"
                    }

                },

                new JournalEntryLineResource
                {
                    GLAccount = "2451",
                    Description = "Segurança Social",
                    DebitAmount = new Amount()
                    {
                        Value = 200,
                        Currency = "€"
                    }

                },

                new JournalEntryLineResource
                {
                    GLAccount = "2311",
                    Description = "Total a pagar ao socio gerente",
                    DebitAmount = new Amount()
                    {
                        Value = 1500,
                        Currency = "€"
                    }

                },

                new JournalEntryLineResource
                {
                    GLAccount = "111",
                    Description = string.Empty,
                    CreditAmount = new Amount()
                    {
                        Value = 4225,
                        Currency = "€"
                    },
                    CashFlowItem = "04",
                    PaymentMethod = "NUM"
                },

                new JournalEntryLineResource
                {
                    GLAccount = "2451",
                    Description = string.Empty,
                    DebitAmount = new Amount()
                    {
                        Value = 425,
                        Currency = "€"
                    }

                }
            };

            return journalEntry;

            #endregion
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
                    Console.WriteLine(string.Concat("Journal entry created: ", result));

                    return (Guid.Parse(result1));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(string.Concat("Failed. ", responseContent.ToString()));
                    string result = await ((StreamContent)responseContent.Content).ReadAsStringAsync();
                    Console.WriteLine(string.Concat("Content: ", result));

                    throw new Exception("Unable to create the journal entry.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Error creating the journal entry.");
            }
        }

        #endregion

        #region GET

        public static async Task GetJournalEntryAsync(AuthData authData)
        {
            Console.WriteLine("Getting Journal Entries...");

            await RequestData("accounting/journalEntries?page=1&pageSize=10", authData);

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

                    List<JournalEntry> journalEntry = oDataResponse.data.ToObject<List<JournalEntry>>();

                    List<(string, string, string, double, double)> lst = new List<(string, string, string, double, double)>();

                    foreach (var i in journalEntry)
                    {
                        lst.Add((i.PostingDate, i.NaturalKey, i.Company, i.TotalCredit.Value, i.TotalDebit.Value));
                    }

                    Console.WriteLine(lst.ToStringTable(
                      new[] { "Posting Date", "Journal", "Company", "T. Credit", "T. Debit"},
                      a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a=> a.Item5));

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
