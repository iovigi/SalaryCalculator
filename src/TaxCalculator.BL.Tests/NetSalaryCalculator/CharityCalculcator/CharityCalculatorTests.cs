using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.BL.NetSalaryCalculator.CharityCalculcator;

namespace TaxCalculator.BL.Tests.NetSalaryCalculator.CharityCalculcator
{
    public class CharityCalculatorTests
    {
        [Test]
        public void GrossSalaryPercentLessThanCharitySpent_Should_ReturnGrossSalaryPercent()
        {
            // Arrange
            var expectedResult = 10;
            CharityCalculator charityCalculator = new CharityCalculator(10);


            // Act
            var result = charityCalculator.CalculateCharityDeduction(100, 200);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GrossSalaryPercentMoreThanCharitySpent_Should_ReturnCharitySpent()
        {
            // Arrange
            var expectedResult = 200;
            CharityCalculator charityCalculator = new CharityCalculator(10);


            // Act
            var result = charityCalculator.CalculateCharityDeduction(3000, 200);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
