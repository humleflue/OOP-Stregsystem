using System.Collections.Generic;
using DashSystem.Controllers;
using DashSystem.Models.Users;
using NUnit.Framework;

namespace DashSystem.Tests.Integration_tests
{
    class UserControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FetchUsers_ValidData_ShouldNotThrow()
        {
            // Arrange
            UserController controller = new UserController();
            // Act
            void FetchUsers() => controller.Fetch();
            // Assert
            Assert.DoesNotThrow(FetchUsers);
        }
    }
}
