namespace SalaryCalculator.Logic.Tests
{
    using System;
    using NUnit.Framework;

    using Mocks;

    public class NetSalaryCalculatorTests
    {
        [Test]
        public void SalaryAfterCalculationShouldBeTheSame()
        {
            var grossSalary = 2000;

            NetSalaryCalculator salaryCalculator = new NetSalaryCalculator();
            var calculatedSalary = salaryCalculator.CalculateNetSalary(grossSalary);

            Assert.AreEqual(grossSalary, calculatedSalary);
        }

        [Test]
        public void SalaryAfterCalculationShouldBeTenPercentLower()
        {
            var grossSalary = 2000;

            NetSalaryCalculator salaryCalculator = new NetSalaryCalculator(new MockTaxationRule());
            var calculatedSalary = salaryCalculator.CalculateNetSalary(grossSalary);

            decimal expectedSalary = 1800m;

            Assert.AreEqual(expectedSalary, calculatedSalary);
        }

        [Test]
        public void ForNegativeSalaryShouldThrowArgumentException()
        {
            var grossSalary = -100;

            NetSalaryCalculator salaryCalculator = new NetSalaryCalculator();

            Assert.Catch<ArgumentException>(() =>
            {
                salaryCalculator.CalculateNetSalary(grossSalary);
            });
        }
    }
}