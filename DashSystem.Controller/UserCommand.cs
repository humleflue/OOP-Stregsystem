using System;
using System.Linq;
using DashSystem.Models.Products;
using DashSystem.Models.Users;

namespace DashSystem.Controller
{
    public class UserCommand
    {
        public IDashSystem DashSystem { get; }
        public string CommandString { get; set; } = null;
        public string[] Argv { get; set; } = null;
        public IUser User { get; set; } = null;
        public IProduct Product { get; set; } = null;
        public uint PurchaseAmount { get; set; } = 0;

        public UserCommand(string commandString, IDashSystem dashSystem)
        {
            CommandString = commandString;
            Argv = commandString.Split(' ');
            DashSystem = dashSystem;

            if (string.IsNullOrEmpty(commandString))
            {
                throw new ArgumentException("Empty command provided as argument");
            }
            
            switch (Argv.Length)
            {
                case 3: PurchaseAmount = ParseAmount(Argv[2]);
                    goto case 2;
                case 2: Product = ParseProduct(Argv[1]);
                    goto case 1;
                case 1: User = ParseUser(Argv[0]);
                    break;
            }
        }

        private uint ParseAmount(string amountString)
        {
            if (!uint.TryParse(amountString, out uint amount))
            {
                return 0;
            }

            return amount;
        }
        
        private IProduct ParseProduct(string productIDString)
        {
            if (!uint.TryParse(productIDString, out uint productID))
            {
                return null;
            }

            return DashSystem.GetProductByID(productID);
        }
        
        private IUser ParseUser(string username)
        {
            return DashSystem.GetUserByUsername(username);
        }

        public bool IsUserInfo()
        {
            return Argv.Length == 1;
        }
        
        public bool IsSinglePurchase()
        {
            return Argv.Length == 2;
        }
        
        public bool IsMultiPurchase()
        {
            return Argv.Length == 3;
        }
    }
}