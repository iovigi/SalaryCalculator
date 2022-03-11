using System;
using NUnit.Framework;
using TaxCalculator.BL.NetSalaryCalculator.TaxRules;

namespace TaxCalculator.BL.Tests.NetSalaryCalculator.TaxRules
{
    public class IncomeTaxationRuleTests
    {
        [Test]
        public void NegativePercent_Should_ThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                IncomeTaxationRule rule = new IncomeTaxationRule(100, -1);
            });
        }

        [Test]
        public void NegativeBelowTaxSalaryLevel_Should_ThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                IncomeTaxationRule rule = new IncomeTaxationRule(-1, 10);
            });
        }

        [Test]
        public void Tax_Should_BeZero()
        {
            // Arrange
            IncomeTaxationRule rule = new IncomeTaxationRule(decimal.MaxValue, 10);
            decimal grossSalary = 10;
            decimal expectedTax = 0m;

            // Act
            var result = rule.CalculateTax(grossSalary);

            // Assert
            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void Tax_Should_BeFullAmountOfSalary()
        {
            // Arrange
            IncomeTaxationRule rule = new IncomeTaxationRule(0, 100);
            decimal grossSalary = 100m;
            decimal expectedTax = 100m;

            // Act
            var result = rule.CalculateTax(grossSalary);

            // Assert
            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void Tax_Should_BeTenPercentOfSalary()
        {
            // Arrange
            IncomeTaxationRule rule = new IncomeTaxationRule(0, 10);
            decimal grossSalary = 100m;
            decimal expectedTax = 10m;

            // Act
            var result = rule.CalculateTax(grossSalary);
            
            // Assert
            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void ForNegativeSalary_Should_ThrowArgumentException()
        {
            // Arrange
            var grossSalary = -100;

            IncomeTaxationRule rule = new IncomeTaxationRule(0, 100);

            //Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                rule.CalculateTax(grossSalary);
            });
        }
    }
}
