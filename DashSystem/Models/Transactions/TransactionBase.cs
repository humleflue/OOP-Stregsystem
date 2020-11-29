using DashSystem.Controllers;
using DashSystem.Models.Users;
using System;
using System.Collections.Generic;

namespace DashSystem.Models.Transactions
{
    public abstract class TransactionBase : ITransaction
    {
        public event Action<ITransaction> NewTransactionExecutedEvent;

        public uint ID { get; private set; }
        public IUser User { get; }
        public DateTime Date { get; }
        // Note: Amount can be negative, if it is going to be withdrawed from from the User's account
        public decimal Amount { get; }

        private static uint nextUID = new TransactionController().GetNextUID();
        private static uint NextUID => nextUID++;

        protected TransactionBase(IUser user, DateTime date, decimal amount)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Date = date;
            Amount = amount;
        }

        protected TransactionBase(uint id, IUser user, DateTime date, decimal amount)
            : this(user, date, amount)
        {
            ID = id > 0 ? id : throw new ArgumentException("ID has to be greater than 0.");
        }

        public virtual void Execute()
        {
            // If ID == 0 it means that the ID haven't been set in the constructor
            // which means that this is a new transaction
            if (ID == 0)
            {
                ID = NextUID;
            }
            User.Balance += Amount;

            NewTransactionExecutedEvent?.Invoke(this);
        }

        public override string ToString()
        {
            return $"{Date}: <{ID}> {User.Username} ({Amount} kr)";
        }

        public virtual List<string> GetCollumnNames()
        {
            return new List<string>() { $"{ID}", $"{GetType().Name}", $"{Date}", $"{Amount}", $"{User.ID}" };
        }
    }
}