using DashSystem.Exceptions;
using DashSystem.Models.Products;
using DashSystem.Models.Transactions;
using DashSystem.Models.Users;
using NSubstitute;
using NUnit.Framework;
using System;

/* Method Naming Conventions. Should be separated by '_':
 * - The name of the method being tested.
 * - The scenario under which it's being tested.
 * - The expected behavior when the scenario is invoked.
 */

namespace DashSystem.Tests
{
    public class BuyTransactionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Execute_InsufficientCredits_ThrowInsufficientCreditsException(decimal userBalance)
        {
            // Arrange
            IUser user = Substitute.For<IUser>();
            IProduct product = Substitute.For<IProduct>();
            product.IsActive = true;
            decimal productPrice = userBalance + 10; // Make price greater than userBalance

            user.Balance = userBalance;
            product.Price = productPrice;
            ITransaction transaction = new BuyTransaction(user, DateTime.Now, product);
            // Act
            void ExecuteTransaction() => transaction.Execute();
            // Assert
            Assert.Throws<InsufficientCreditsException>(ExecuteTransaction);
        }

        [Test]
        public void Execute_SufficientCredits_WidtdrawsTransactionAmountFromUserBalance()
        {
            // Arrange
            IUser user = Substitute.For<IUser>();
            IProduct product = new Product(1000, "Name", 1000, true);

            decimal startBalance = 100m;
            user.Balance = startBalance;
            decimal productPrice = 10m;
            product.Price = productPrice;
            
            ITransaction transaction = new BuyTransaction(user, DateTime.Now, product);
            // Act
            transaction.Execute();
            // Assert
            Assert.AreEqual(startBalance - productPrice, user.Balance);
        }
    }
}