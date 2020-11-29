using DashSystem.Models.Users;
using NUnit.Framework;
using System;

// ReSharper disable ObjectCreationAsStatement

/* Method Naming Conventions. Should be separated by '_':
 * - The name of the method being tested.
 * - The scenario under which it's being tested.
 * - The expected behavior when the scenario is invoked.
 */

namespace DashSystem.Tests
{
    class UserTests
    {
        public uint SampleID { get; } = 1;
        public int SampleBallance { get; } = 0;
        public string SampleFirstName { get; } = "Foo";
        public string SampleLastName { get; } = "Bar";
        public string SampleUsername { get; set; } = "Baz";

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("smallletters")]
        [TestCase("CAPITALLETTERS")]
        [TestCase("1234567890")]
        [TestCase("d.o.t.s")]
        [TestCase("u_n_d_e_r_s_c_o_r_e_s")]
        [TestCase("d-a-s-h-e-s")]
        public void UserConstructor_ValidEmailLocalPart_ShouldNotThrow(string localPart)
        {
            // Arrange
            string email = $"{localPart}@domain.com";
            // Act
            void ConstructUser() => new User(SampleID, SampleFirstName, SampleLastName, SampleUsername, SampleBallance, email);
            // Assert
            Assert.DoesNotThrow(ConstructUser);
        }

        [TestCase("smallletters.com")]
        [TestCase("CAPITALLETTERS.com")]
        [TestCase("1234567890.com")]
        [TestCase("d.o.t.s.com")]
        [TestCase("d-a-s-h-e-s.com")]
        public void UserConstructor_ValidEmailDomain_ShouldNotThrow(string domain)
        {
            // Arrange
            string email = $"localPart@{domain}";
            // Act
            void ConstructUser() => new User(SampleID, SampleFirstName, SampleLastName, SampleUsername, SampleBallance, email);
            // Assert
            Assert.DoesNotThrow(ConstructUser);
        }

        [TestCase(".startsWithDot.com")]
        [TestCase("-startsWithDash.com")]
        [TestCase("endsWithDot.com.")]
        [TestCase("endsWithDash.com-")]
        [TestCase("doesNotContainAnyDot")]
        [TestCase("contains@character")]
        public void UserConstructor_InvalidEmailDomain_ThrowArgumentException(string domain)
        {
            // Arrange
            string email = $"localPart@{domain}";
            // Act
            void ConstructUser() => new User(SampleID, SampleFirstName, SampleLastName, SampleUsername, SampleBallance, email);
            // Assert
            Assert.Throws<ArgumentException>(ConstructUser);
        }

        [Test]
        public void UserConstructor_NoAtCharacterInEmail_ThrowArgumentException()
        {
            // Arrange
            const string email = "NoAtCharacterInEmail.com";
            // Act
            void ConstructUser() => new User(SampleID, SampleFirstName, SampleLastName, SampleUsername, SampleBallance, email);
            // Assert
            Assert.Throws<ArgumentException>(ConstructUser);
        }

        [Test]
        public void UserConstructor_EmailIsNull_ThrowNullReferenceException()
        {
            // Arrange
            const string email = null;
            // Act
            void ConstructUser() => new User(SampleID, SampleFirstName, SampleLastName, SampleUsername, SampleBallance, email);
            // Assert
            Assert.Throws<NullReferenceException>(ConstructUser);
        }
    }

}
