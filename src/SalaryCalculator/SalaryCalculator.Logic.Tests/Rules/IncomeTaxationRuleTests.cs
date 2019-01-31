namespace SalaryCalculator.Logic.Tests.Rules
{
    using System;
    using NUnit.Framework;

    using Logic.Rules;

    public class IncomeTaxationRuleTests
    {
        [Test]
        public void NegativePercentShouldThrowArgumentException()
        {
            Assert.Catch<ArgumentException>(() =>
            {
                IncomeTaxationRule rule = new IncomeTaxationRule(100, -1);
            });
        }

        [Test]
        public void NegativeBelowTaxSalaryLevelShouldThrowArgumentException()
        {
            Assert.Catch<ArgumentException>(() =>
            {
                IncomeTaxationRule rule = new IncomeTaxationRule(-1, 10);
            });
        }

        [Test]
        public void TaxShouldBeZero()
        {
            IncomeTaxationRule rule = new IncomeTaxationRule(decimal.MaxValue, 10);
            decimal grossSalary = 10;
            var result = rule.CalculateTax(grossSalary);
            decimal expectedTax = 0m;

            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void TaxShouldBeFullAmountOfSalary()
        {
            IncomeTaxationRule rule = new IncomeTaxationRule(0, 100);
            decimal grossSalary = 100m;
            var result = rule.CalculateTax(grossSalary);
            decimal expectedTax = 100m;

            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void TaxShouldBeTenPercentOfSalary()
        {
            IncomeTaxationRule rule = new IncomeTaxationRule(0, 10);
            decimal grossSalary = 100m;
            var result = rule.CalculateTax(grossSalary);
            decimal expectedTax = 10m;

            Assert.AreEqual(expectedTax, result);
        }

        [Test]
        public void ForNegativeSalaryShouldThrowArgumentException()
        {
            var grossSalary = -100;

            IncomeTaxationRule rule = new IncomeTaxationRule(0, 100);

            Assert.Catch<ArgumentException>(() =>
            {
                rule.CalculateTax(grossSalary);
            });
        }
    }
}
