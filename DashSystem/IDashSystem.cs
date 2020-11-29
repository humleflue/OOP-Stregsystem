using DashSystem.Models.Products;
using DashSystem.Models.Transactions;
using DashSystem.Models.Users;
using System;
using System.Collections.Generic;
using DashSystem.Controllers;

namespace DashSystem
{
    public interface IDashSystem
    {
        public event UserBalanceNotification UserBalanceWarning;
        public IEnumerable<IProduct> ActiveProducts { get; }
        public bool Debug { get; set; }

        public BuyTransaction CreateBuyTransaction(IUser user, IProduct product);
        public InsertCashTransaction CreateInsertCashTransaction(IUser user, decimal amount);
        public void ExecuteTransaction(ITransaction transaction);
        public IProduct GetProductByID(uint id);
        public IEnumerable<IUser> GetUsers(Func<IUser, bool> predicate);
        public IUser GetUserByUsername(string username);
        public IEnumerable<ITransaction> GetTransactions(IUser user, int count);
    }

}
