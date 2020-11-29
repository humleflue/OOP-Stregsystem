using DashSystem.Exceptions;
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
    public class InsertCashTransactionTests
    {
        public uint SampleID { get; } = 1;
        public DateTime SampleDate { get; set; } = new DateTime(2000, 1, 1);

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Execute_InvalidCashAmount_ThrowInvalidCashAmountException(decimal cashAmount)
        {
            // Arrange
            IUser user = Substitute.For<IUser>();
            ITransaction transaction = new InsertCashTransaction(SampleID, user, SampleDate, cashAmount);
            // Act
            void ExecuteTransaction() => transaction.Execute();
            // Assert
            Assert.Throws<InvalidCashAmountException>(ExecuteTransaction);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)] // Shouldn't be possible in the first place
        public void Execute_ValidCashAmount_AddCashAmountToUserBalance(decimal initialUserBalance)
        {
            // Arrange
            IUser user = Substitute.For<IUser>();

            user.Balance = initialUserBalance;
            decimal cashToBeInserted = 10m;
            ITransaction transaction = new InsertCashTransaction(SampleID, user, SampleDate, cashToBeInserted);
            // Act
            transaction.Execute();
            // Assert
            Assert.AreEqual(initialUserBalance + cashToBeInserted, user.Balance);
        }
    }
}