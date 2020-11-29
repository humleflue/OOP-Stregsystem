using DashSystem.Exceptions;
using DashSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashSystem.Models.Transactions
{
    public sealed class InsertCashTransaction : TransactionBase
    {
        public InsertCashTransaction(uint id, IUser user, DateTime date, decimal amount)
            : base(id, user, date, amount)
        {
        }
        public InsertCashTransaction(IUser user, DateTime date, decimal amount)
            : base(user, date, amount)
        {
        }

        public override void Execute()
        {
            if (Amount <= 0)
            {
                throw new InvalidCashAmountException($"Deposit amount ({Amount}) has to be greater than 0.");
            }
            base.Execute();
        }

        public override string ToString()
        {
            return $"Cash inserted: {base.ToString()}";
        }

        public override List<string> GetCollumnNames()
        {
            List<string> collumnNames = base.GetCollumnNames().ToList(); // Copy
            collumnNames.Add(""); // No product in a cash transaction
            return collumnNames;
        }
    }
}