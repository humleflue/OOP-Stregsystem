using DashSystem.Models.Transactions;
using DashSystem.Models.Users;
using System;
using System.Collections.Generic;
using DashSystem.Models.Products;

namespace DashSystem.UI
{
    public class DashSystemCLI : IDashSystemUI
    {
        public event CommandEntered CommandEnteredEvent;
        public bool Done { get; private set; }
        public IDashSystem DashSystem { get; }

        public DashSystemCLI(IDashSystem dashSystem)
        {
            DashSystem = dashSystem;
            DashSystem.UserBalanceWarning += DisplayUserBalanceWarning;
        }
        public void Start()
        {
            Console.WriteLine("Welcome to DashSystemCLI!\n" +
                              "Active products:");
            foreach (IProduct product in DashSystem.ActiveProducts)
            {
                Console.WriteLine(product);
            }

            while (!Done)
            {
                Show();
                HandleInput();
            }
        }

        private void Show()
        {
            Console.WriteLine("\nEnter a command:");
        }

        private void HandleInput()
        {
            string input = Console.ReadLine();
            CommandEnteredEvent?.Invoke(input);
        }

        public void Close()
        {
            Done = true;
        }

        #region REGULAR_MESSAGING
        public void DisplayGeneralMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayUserInfo(IUser user)
        {
            DisplayGeneralMessage($"{user} has {user.Balance} DKK left to spend.");
            // Print last 10 transactions
            IEnumerable<ITransaction> transactions = DashSystem.GetTransactions(user, 10);
            DisplayGeneralMessage(">>> Purchased products <<<");
            foreach (ITransaction transaction in transactions)
            {
                DisplayGeneralMessage($"{transaction}");
            }

            if (user.Balance < 50)
            {
                DisplayUserBalanceWarning(user);
            }
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            DisplayUserBuysMultipleProducts(1, transaction);
        }

        public void DisplayUserBuysMultipleProducts(uint count, BuyTransaction transaction)
        {
            DisplayGeneralMessage($"{transaction.User} just bought {count} {transaction.Product.Name}.");
        }
        #endregion

        #region ERROR_MESSAGING
        public void DisplayGeneralError(string errorMessage)
        {
            Console.WriteLine($"ERROR: {errorMessage}");
        }

        public void DisplayUserNotFound(string username)
        {
            DisplayGeneralError($"Not able to find a User with username: {username}.");
        }

        public void DisplayProductNotFound(uint productID)
        {
            DisplayGeneralError($"Not able to find product with ID: {productID}.");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            DisplayGeneralError($"Too many arguments in command <{command}>.");
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            DisplayGeneralError($"No such admin command: {adminCommand}.");
        }

        public void DisplayIntegersMustBePositive(int argvIndex)
        {
            DisplayGeneralError($"Argument {argvIndex} must be positive integers.");
        }
        #endregion

        #region WARNING_MESSAGING

        public void DisplayGeneralWarning(string warningMessage)
        {
            DisplayGeneralMessage($"WARNING: {warningMessage}");
        }

        public void DisplayUserBalanceWarning(IUser user)
        {
            DisplayGeneralWarning($"{user}'s ballance is critically low ({user.Balance}).");
        }
        #endregion
    }
}
