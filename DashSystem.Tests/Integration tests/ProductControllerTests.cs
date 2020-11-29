using DashSystem.Controllers;
using NUnit.Framework;

namespace DashSystem.Tests.Integration_tests
{
    class ProductControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FetchProducts_ValidData_ShouldNotThrow()
        {
            // Arrange
            ProductController controller = new ProductController();
            // Act
            void FetchProducts() => controller.Fetch();
            // Assert
            Assert.DoesNotThrow(FetchProducts);
        }
    }
}
