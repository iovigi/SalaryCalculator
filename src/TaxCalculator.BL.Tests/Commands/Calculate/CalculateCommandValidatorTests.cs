using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.BL.Commands.Calculate;

namespace TaxCalculator.BL.Tests.Commands.Calculate
{
    public class CalculateCommandValidatorTests
    {
        [TestCase("test")]
        [TestCase("test test ")]
        [TestCase(" test test")]
        [TestCase("te2st test")]
        public void FullnameInvalid_Should_Not_Be_Valid(string fullName)
        {
            // Arrange
            var command = new CalculateCommand();
            command.FullName = fullName;
            command.SSN = "12345";
            command.GrossIncome = 100;
            CalculateCommandValidator validator = new CalculateCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("FullName", result.Errors[0].PropertyName);
        }

        [TestCase("test")]
        [TestCase("123")]
        [TestCase("12345678910")]
        public void SSNInvalid_Should_Not_Be_Valid(string ssn)
        {
            // Arrange
            var command = new CalculateCommand();
            command.FullName = "test test";
            command.SSN = ssn;
            command.GrossIncome = 100;
            CalculateCommandValidator validator = new CalculateCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("SSN", result.Errors[0].PropertyName);
        }

        [Test]
        public void GrossIncomeInvalid_Should_Not_Be_Valid()
        {
            // Arrange
            var command = new CalculateCommand();
            command.FullName = "test test";
            command.SSN = "12345";
            command.GrossIncome = -100;
            CalculateCommandValidator validator = new CalculateCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("GrossIncome", result.Errors[0].PropertyName);
        }

        [Test]
        public void CharitySpentInvalid_Should_Not_Be_Valid()
        {
            // Arrange
            var command = new CalculateCommand();
            command.FullName = "test test";
            command.SSN = "12345";
            command.GrossIncome = 100;
            command.CharitySpent = -100;
            CalculateCommandValidator validator = new CalculateCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("CharitySpent", result.Errors[0].PropertyName);
        }

        [TestCase("12345")]
        [TestCase("1234567891")]
        public void SSNValid_Should_Be_Valid(string ssn)
        {
            // Arrange
            var command = new CalculateCommand();
            command.FullName = "test test";
            command.SSN = ssn;
            command.GrossIncome = 100;
            CalculateCommandValidator validator = new CalculateCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestCase(0)]
        [TestCase(100)]
        public void GrossIncomeValid_Should_Be_Valid(decimal gross)
        {
            // Arrange
            var command = new CalculateCommand();
            command.FullName = "test test";
            command.SSN = "12345";
            command.GrossIncome = gross;
            CalculateCommandValidator validator = new CalculateCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestCase(0)]
        [TestCase(100)]
        public void CharitySpentValid_Should_Be_Valid(decimal charitySpent)
        {
            // Arrange
            var command = new CalculateCommand();
            command.FullName = "test test";
            command.SSN = "12345";
            command.GrossIncome = 100;
            command.CharitySpent = charitySpent;
            CalculateCommandValidator validator = new CalculateCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestCase("test test test")]
        [TestCase("test test")]
        public void FullnameValid_Should_Be_Valid(string fullName)
        {
            // Arrange
            var command = new CalculateCommand();
            command.FullName = fullName;
            command.SSN = "12345";
            command.GrossIncome = 100;
            CalculateCommandValidator validator = new CalculateCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Errors.Count);
        }
    }
}
