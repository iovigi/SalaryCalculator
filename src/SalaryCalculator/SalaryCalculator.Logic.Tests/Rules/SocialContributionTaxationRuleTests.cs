namespace SalaryCalculator.Logic.Tests.Rules
{
    using System;
    using NUnit.Framework;

    using Logic.Rules;

    public class SocialContributionTaxationRuleTests
    {
        [Test]
        public void NegativePercentShouldThrowArgumentException()
        {
            Assert.Catch<ArgumentException>(() =>
            {
                SocialContributionTaxationRule rule = new SocialContributionTaxationRule(100, 200, -1);
            });
        }

        [Test]
        public void NegativeBelowTaxSalaryLevelShouldThrowArgumentException()
        {
            Assert.Catch<ArgumentException>(() =>
            {
                SocialContributionTaxationRule rule = new SocialContributionTaxationRule(-1, 10, 10);
            });
        }

        [Test]
        public void NegativeHigherSalaryLevelShouldThrowArgumentException()
        {
            Assert.Catch<ArgumentException>(() =>
            {
                SocialContributionTaxationRule rule = new SocialContributionTaxationRule(10, -10, 10);
            });
        }

        [Test]
        public void HigherSalaryLevelLowerThanBelowTaxSalaryLevelShouldThrowArgumentException()
        {
            Assert.Catch<ArgumentException>(() =>
            {
                SocialContributionTaxationRule rule = new SocialContributionTaxationRule(10, 5, 10);
            });
        }

        [Test]
        public void TaxShouldBeZero()
        {
            SocialContributionTaxationRule rule = new SocialContributionTaxationRule(10000m, decimal.MaxValue, 10);
            decimal grossSalary = 10;
            var result = rule.CalculateTax(grossSalary);
            decimal expectedTax = 0m;

            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void TaxShouldBeFullAmountOfSalary()
        {
            SocialContributionTaxationRule rule = new SocialContributionTaxationRule(0, 200, 100);
            decimal grossSalary = 100m;
            var result = rule.CalculateTax(grossSalary);
            decimal expectedTax = 100m;

            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void TaxShouldBeTenPercentOfSalary()
        {
            SocialContributionTaxationRule rule = new SocialContributionTaxationRule(0, 200, 10);
            decimal grossSalary = 100m;
            var result = rule.CalculateTax(grossSalary);
            decimal expectedTax = 10m;

            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void ForNegativeSalaryShouldThrowArgumentException()
        {
            var grossSalary = -100;

            SocialContributionTaxationRule rule = new SocialContributionTaxationRule(0, 100, 100);

            Assert.Catch<ArgumentException>(() =>
            {
                rule.CalculateTax(grossSalary);
            });
        }
    }
}
