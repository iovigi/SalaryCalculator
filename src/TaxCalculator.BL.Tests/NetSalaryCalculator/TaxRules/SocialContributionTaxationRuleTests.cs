using System;
using NUnit.Framework;
using TaxCalculator.BL.NetSalaryCalculator.TaxRules;

namespace TaxCalculator.BL.Tests.NetSalaryCalculator.TaxRules
{
    public class SocialContributionTaxationRuleTests
    {
        [Test]
        public void NegativePercent_Should_ThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                SocialContributionTaxationRule rule = new SocialContributionTaxationRule(100, 200, -1);
            });
        }

        [Test]
        public void NegativeBelowTaxSalaryLevel_Should_ThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                SocialContributionTaxationRule rule = new SocialContributionTaxationRule(-1, 10, 10);
            });
        }

        [Test]
        public void NegativeHigherSalaryLevel_Should_ThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                SocialContributionTaxationRule rule = new SocialContributionTaxationRule(10, -10, 10);
            });
        }

        [Test]
        public void HigherSalaryLevelLowerThanBelowTaxSalaryLevel_Should_ThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                SocialContributionTaxationRule rule = new SocialContributionTaxationRule(10, 5, 10);
            });
        }

        [Test]
        public void Tax_Should_BeZero()
        {
            // Arrange
            SocialContributionTaxationRule rule = new SocialContributionTaxationRule(10000m, decimal.MaxValue, 10);
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
            SocialContributionTaxationRule rule = new SocialContributionTaxationRule(0, 200, 100);
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
            SocialContributionTaxationRule rule = new SocialContributionTaxationRule(0, 200, 10);
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
            SocialContributionTaxationRule rule = new SocialContributionTaxationRule(0, 100, 100);

            // Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                rule.CalculateTax(grossSalary);
            });
        }
    }
}
