using DashSystem.Exceptions;
using DashSystem.Models.Products;
using DashSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashSystem.Models.Transactions
{
    public sealed class BuyTransaction : TransactionBase
    {
        public IProduct Product { get; set; }

        public BuyTransaction(uint id, IUser user, DateTime date, IProduct product)
            : base(id, user, date, product.Price * (-1)) // Multiply Price by -1 as it's a withdrawal from the user's account
        {
            Product = product;
        }
        public BuyTransaction(IUser user, DateTime date, IProduct product)
            : base(user, date, product.Price * (-1)) // Multiply Price by -1 as it's a withdrawal from the user's account
        {
            Product = product;
        }

        public override void Execute()
        {
            if (!Product.IsActive)
            {
                throw new ProductInactiveException($"{Product} is inactive. Purchase aborted.");
            }
            if (User.Balance < Math.Abs(Amount) && !Product.CanBeBoughtOnCredit)
            {
                throw new InsufficientCreditsException($"{User.Username} does not have enough credits ({User.Balance}) to buy {Product.Name}.  Price: {Product.Price}");
            }

            base.Execute();
        }

        public override string ToString()
        {
            return $"Product purchased: {base.ToString()} - {Product.Name}";
        }

        public override List<string> GetCollumnNames()
        {
            List<string> collumnNames = base.GetCollumnNames().ToList();
            collumnNames.Add($"{Product.ID}");
            return collumnNames;
        }
    }
}