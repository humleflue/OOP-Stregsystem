using DashSystem.Models.Users;
using System;

namespace DashSystem.Models.Transactions
{
    public interface ITransaction : ICollumnNameGetable
    {
        public event Action<ITransaction> NewTransactionExecutedEvent;
        public uint ID { get; }
        public IUser User { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }
        public void Execute();
    }
}