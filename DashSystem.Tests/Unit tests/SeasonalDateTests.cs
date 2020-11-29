using DashSystem.Models.Products.SeasonalProducts;
using NUnit.Framework;

/* Method Naming Conventions. Should be separated by '_':
 * - The name of the method being tested.
 * - The scenario under which it's being tested.
 * - The expected behavior when the scenario is invoked.
 */
namespace DashSystem.Tests
{
    class SeasonalDateTests
    {
        // TODO: LowPriority: Find another approach than hardcoded values
        [TestCase(1, 1)]
        [TestCase(5, 1)]
        [TestCase(10, 1)]
        [TestCase(1, 5)]
        [TestCase(1, 10)]
        [TestCase(5, 10)]
        public void IsWithinRangeOf_IsWithinNaturalRange_ReturnsTrue(int day, int month)
        {
            // Arrange
            SeasonalDate lowerBound = new SeasonalDate(1, 1);
            SeasonalDate upperBound = new SeasonalDate(5, 10);
            SeasonalDate date = new SeasonalDate(day, month);
            // Act
            bool isWithinTheRange = date.IsWithinRangeOf(lowerBound, upperBound);
            // Assert
            Assert.True(isWithinTheRange);
        }

        // TODO: LowPriority: Find another approach than hardcoded values
        [TestCase(4, 1)]
        [TestCase(10, 1)]
        [TestCase(1, 4)]
        [TestCase(4, 4)]
        [TestCase(10, 10)]
        public void IsWithinRangeOf_IsWithinRangeWhichSuperseedsNewYear_ReturnsTrue(int day, int month)
        {
            // Arrange
            SeasonalDate lowerBound = new SeasonalDate(10, 10);
            SeasonalDate upperBound = new SeasonalDate(4, 4);
            SeasonalDate date = new SeasonalDate(day, month);
            // Act
            bool isWithinTheRange = date.IsWithinRangeOf(lowerBound, upperBound);
            // Assert
            Assert.True(isWithinTheRange);
        }

        // TODO: LowPriority: Find another approach than hardcoded values
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        [TestCase(6, 10)]
        [TestCase(5, 11)]
        public void IsWithinRangeOf_IsNotWithinNaturalRange_ReturnsFalse(int day, int month)
        {
            // Arrange
            SeasonalDate lowerBound = new SeasonalDate(2, 2);
            SeasonalDate upperBound = new SeasonalDate(5, 10);
            SeasonalDate date = new SeasonalDate(day, month);
            // Act
            bool isWithinTheRange = date.IsWithinRangeOf(lowerBound, upperBound);
            // Assert
            Assert.False(isWithinTheRange);
        }

        // TODO: LowPriority: Find another approach than hardcoded values
        [TestCase(5, 4)]
        [TestCase(10, 4)]
        [TestCase(10, 9)]
        [TestCase(4, 10)]
        [TestCase(9, 10)]
        public void IsWithinRangeOf_IsNotWithinRangeWhichSuperseedsNewYear_ReturnsFalse(int day, int month)
        {
            // Arrange
            SeasonalDate lowerBound = new SeasonalDate(10, 10);
            SeasonalDate upperBound = new SeasonalDate(4, 4);
            SeasonalDate date = new SeasonalDate(day, month);
            // Act
            bool isWithinTheRange = date.IsWithinRangeOf(lowerBound, upperBound);
            // Assert
            Assert.False(isWithinTheRange);
        }
    }
}
