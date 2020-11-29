using System;
using DashSystem.Controllers;
using DashSystem.Models.Products;
using DashSystem.Models.Transactions;
using DashSystem.Models.Users;
using NSubstitute;
using NUnit.Framework;

/* Method Naming Conventions. Should be separated by '_':
 * - The name of the method being tested.
 * - The scenario under which it's being tested.
 * - The expected behavior when the scenario is invoked.
 */

namespace DashSystem.Tests.Integration_tests
{
    class TransactionControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FetchTransactions_ValidData_ShouldNotThrow()
        {
            // Arrange
            TransactionController controller = new TransactionController();
            // Act
            void FetchTransactions() => controller.Fetch();
            // Assert
            Assert.DoesNotThrow(FetchTransactions);
        }
    }
}
