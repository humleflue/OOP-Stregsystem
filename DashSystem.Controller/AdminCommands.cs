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
        private class AdminCommands
        {
            private IDashSystemUI UI { get; }
            private IDashSystem DashSystem { get; }

            public AdminCommands(IDashSystemUI ui, IDashSystem dashSystem)
            {
                UI = ui;
                DashSystem = dashSystem;
            }

            public void Quit(string[] argv)
            {
                UI.Close();
                UI.DisplayGeneralMessage("Bye.");
            }

            public void Activate(string[] argv)
            {
                if (!uint.TryParse(argv[1], out uint productID))
                    throw new FormatException($"{argv[1]} is not a valid product ID.");

                IProduct product = DashSystem.GetProductByID(productID);
                product.IsActive = true;

                UI.DisplayGeneralMessage($"Activated {product}.");
            }

            public void Deactivate(string[] argv)
            {
                if (!uint.TryParse(argv[1], out uint productID))
                    throw new FormatException($"{argv[1]} is not a valid product ID.");

                IProduct product = DashSystem.GetProductByID(productID);
                product.IsActive = false;

                UI.DisplayGeneralMessage($"Deactivated {product}.");
            }

            public void CreditOn(string[] argv)
            {
                if (!uint.TryParse(argv[1], out uint productID))
                    throw new FormatException($"{argv[1]} is not a valid product ID.");

                IProduct product = DashSystem.GetProductByID(productID);
                product.CanBeBoughtOnCredit = true;

                UI.DisplayGeneralMessage($"This product can now be bought on credit: {product}.");
            }

            public void CreditOff(string[] argv)
            {
                if (!uint.TryParse(argv[1], out uint productID))
                    throw new FormatException($"{argv[1]} is not a valid product ID.");

                IProduct product = DashSystem.GetProductByID(productID);
                product.CanBeBoughtOnCredit = false;

                UI.DisplayGeneralMessage($"This product now can't be bought on credit: {product}.");
            }

            public void AddCredits(string[] argv)
            {
                string username = argv[1];
                if (!decimal.TryParse(argv[2], out decimal amount))
                    throw new FormatException($"{argv[2]} is not a valid amount.");

                IUser user = DashSystem.GetUserByUsername(username);
                ITransaction transaction = DashSystem.CreateInsertCashTransaction(user, amount);
                DashSystem.ExecuteTransaction(transaction);

                UI.DisplayGeneralMessage($"Added {amount} to {user}.");
            }

            public void SetDebug(string[] argv)
            {
                if (!bool.TryParse(argv[1], out bool isInDebugMode))
                    throw new FormatException($"{argv[1]} is not a valid boolean.");

                DashSystem.Debug = isInDebugMode;
                UI.DisplayGeneralMessage($"Debug is now set to {isInDebugMode}.");
            }

            public void GetUserByUsername(string[] argv)
            {
                IUser user = DashSystem.GetUserByUsername(argv[1]);
                UI.DisplayUserInfo(user);
            }

            public void GetUsersWithCriticalBalance(string[] argv)
            {
                IEnumerable<IUser> usersWithCriticalBalance = DashSystem.GetUsers(x => x.Balance < 50);
                UI.DisplayGeneralMessage("Users with critically low balance:");
                foreach (IUser user in usersWithCriticalBalance)
                {
                    UI.DisplayUserInfo(user);
                }
            }
        }
    }

}
