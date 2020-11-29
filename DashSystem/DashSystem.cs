using DashSystem.Controllers;
using DashSystem.Models.Products;
using DashSystem.Models.Transactions;
using DashSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashSystem
{
    public class DashSystem : IDashSystem
    {
        public event UserBalanceNotification UserBalanceWarning;
        public IEnumerable<IProduct> ActiveProducts => Products.Where(x => x.IsActive);
        public bool Debug { get; set; } = false;

        #region PRIVATE_PROPERTIES
        private IController<IUser> UserController { get; } = new UserController();
        private List<IUser> Users => UserController.Fetch();
        private IController<IProduct> ProductController { get; } = new ProductController();
        private List<IProduct> Products => ProductController.Fetch();
        private IController<ITransaction> TransactionController { get; } = new TransactionController();
        private List<ITransaction> Transactions => TransactionController.Fetch();
        #endregion

        public DashSystem()
        {
            foreach (IUser user in Users)
            {
                user.UserBalanceChangedEvent += OnUserBalanceChangedEvent;
            }
        }

        private void OnUserBalanceChangedEvent(IUser user)
        {
            if (user.Balance < 50)
            {
                UserBalanceWarning?.Invoke(user);
            }
        }

        public BuyTransaction CreateBuyTransaction(IUser user, IProduct product)
        {
            BuyTransaction transaction = new BuyTransaction(user, DateTime.Now, product);
            transaction.NewTransactionExecutedEvent += TransactionController.Add;

            return transaction;
        }

        public InsertCashTransaction CreateInsertCashTransaction(IUser user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, DateTime.Now, amount);
            transaction.NewTransactionExecutedEvent += TransactionController.Add;

            return transaction;
        }

        public void ExecuteTransaction(ITransaction transaction)
        {
            transaction.Execute();
        }

        public IProduct GetProductByID(uint id)
        {
            return Products.Find(x => x.ID == id) ?? throw new ArgumentException($"There's no product with id {id}");
        }

        public IEnumerable<IUser> GetUsers(Func<IUser, bool> predicate)
        {
            return Users.Where(predicate);
        }

        public IUser GetUserByUsername(string username)
        {
            return Users.Find(x => x.Username == username) ?? throw new ArgumentException($"There's no user with username {username}");
        }

        public IEnumerable<ITransaction> GetTransactions(IUser user, int count)
        {
            return Transactions.Where(x => x.User.Equals(user)).OrderByDescending(x => x.Date).Take(count);
        }
    }
}
