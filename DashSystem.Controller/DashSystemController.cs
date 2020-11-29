using DashSystem.Exceptions;
using DashSystem.Models.Products;
using DashSystem.Models.Transactions;
using DashSystem.Models.Users;
using DashSystem.UI;
using System;
using System.Collections.Generic;

namespace DashSystem.Controller
{
    partial class DashSystemController
    {
        private Dictionary<string, Action<string[]>> AdminCommandLookup { get; }
        private IDashSystemUI UI { get; }
        private IDashSystem DashSystem { get; }

        public DashSystemController(IDashSystemUI ui, IDashSystem dashSystem)
        {
            UI = ui;
            DashSystem = dashSystem;

            ui.CommandEnteredEvent += OnCommandEntered;

            AdminCommands ac = new AdminCommands(UI, DashSystem);
            AdminCommandLookup = new Dictionary<string, Action<string[]>>
            {
                { ":q",          ac.Quit },
                { ":quit",       ac.Quit },
                { ":activate",   ac.Activate },
                { ":deactivate", ac.Deactivate },
                { ":crediton",   ac.CreditOn },
                { ":creditoff",  ac.CreditOff },
                { ":addcredits", ac.AddCredits },
                { ":debug",      ac.SetDebug },
                { ":critical",   ac.GetUsersWithCriticalBalance },
                { ":getuser",    ac.GetUserByUsername }
            };
        }

        private void OnCommandEntered(string command)
        {
            if (DashSystem.Debug)
            {
                ExecuteCommand(command);
            }
            else
            {
                try
                {
                    ExecuteCommand(command);
                }
                catch (Exception e)
                {
                    UI.DisplayGeneralError(e.Message);
                }
            }
        }

        private void ExecuteCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                UI.DisplayGeneralError("Please enter something.");
            }
            else if (command.StartsWith(':'))
            {
                InvokeAdminCommand(command);
            }
            else
            {
                InvokeUserCommand(command);
            }
        }

        private void InvokeAdminCommand(string command)
        {
            string[] argv = command.Split(' ');

            try
            {
                AdminCommandLookup[argv[0]].Invoke(argv);
            }
            catch (KeyNotFoundException)
            {
                UI.DisplayAdminCommandNotFoundMessage(argv[0]);
            }
        }

        private void InvokeUserCommand(string commandString)
        {
            UserCommand command = new UserCommand(commandString, DashSystem);
            
            if (command.IsUserInfo())
            {
                UI.DisplayUserInfo(command.User);   
            }
            else if (command.IsSinglePurchase())
            {
                BuyTransaction transaction = DashSystem.CreateBuyTransaction(command.User, command.Product);
                DashSystem.ExecuteTransaction(transaction);
                UI.DisplayUserBuysProduct(transaction);
            }
            else if (command.IsMultiPurchase())
            {
                BuyMultipleProducts(command);
            }
            else
            {
                UI.DisplayTooManyArgumentsError(command.CommandString);
            }
        }

        private void BuyMultipleProducts(UserCommand command)
        {
            BuyTransaction testTransaction = DashSystem.CreateBuyTransaction(command.User, command.Product);                                        
            decimal transactionPrice = Math.Abs(testTransaction.Amount) * command.PurchaseAmount;
            
            if (transactionPrice < testTransaction.User.Balance || testTransaction.Product.CanBeBoughtOnCredit)
            {
                List<BuyTransaction> transactions = new List<BuyTransaction>();
                for (int i = 0; i < command.PurchaseAmount; i++)
                {
                    BuyTransaction transaction = DashSystem.CreateBuyTransaction(command.User, command.Product);
                    transactions.Add(transaction);
                }
                
                transactions.ForEach(x => x.Execute());
                UI.DisplayUserBuysMultipleProducts(command.PurchaseAmount, transactions[0]);
            
            }
            else
            {
                throw new InsufficientCreditsException(
                    $"{testTransaction.User.Username} does not have enough credits ({testTransaction.User.Balance}) " +
                    $"to buy {command.PurchaseAmount} {testTransaction.Product.Name}. Price: {transactionPrice}");
            }    
        }
    }
}
