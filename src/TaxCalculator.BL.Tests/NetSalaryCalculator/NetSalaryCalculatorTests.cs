using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TaxCalculator.BL.NetSalaryCalculator.CharityCalculcator;
using TaxCalculator.BL.NetSalaryCalculator.TaxRules;

namespace TaxCalculator.BL.Tests.NetSalaryCalculator
{
    public class NetSalaryCalculatorTests
    {
        [Test]
        public void ForNegativeSalary_Should_ThrowArgumentException()
        {
            // Arrange
            var grossSalary = -100;

            var charityCalculatorMock = new Mock<ICharityCalculator>();
            TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator salaryCalculator = new TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator(charityCalculatorMock.Object);

            // Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                salaryCalculator.CalculateNetSalary(grossSalary, null);
            });
        }

        [Test]
        public void ForNegativeCharitySpent_Should_ThrowArgumentException()
        {
            // Arrange
            var grossSalary = 100;

            var charityCalculatorMock = new Mock<ICharityCalculator>();
            TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator salaryCalculator = new TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator(charityCalculatorMock.Object);

            // Act, Assert
            Assert.Catch<ArgumentException>(() =>
            {
                salaryCalculator.CalculateNetSalary(grossSalary, -10);
            });
        }

        [Test]
        public void WithoutRules_Should_ReturnGrossSalary()
        {
            // Arrange
            var grossSalary = 100;

            var charityCalculatorMock = new Mock<ICharityCalculator>();
            TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator salaryCalculator = new TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator(charityCalculatorMock.Object);

            // Act
            (decimal netSalary, Dictionary<string, decimal> taxes, decimal totalTax) = salaryCalculator.CalculateNetSalary(grossSalary, null);

            // Assert
            Assert.AreEqual(grossSalary, netSalary);
            Assert.AreEqual(0, taxes.Count);
            Assert.AreEqual(0, totalTax);
        }

        [Test]
        public void WithCharitySpent_Should_GrossSalaryBeDeducted()
        {
            // Arrange
            var grossSalary = 100;
            var charity = 50;

            var charityCalculatorMock = new Mock<ICharityCalculator>();
            var callCalculateCharityDeduction = charityCalculatorMock.Setup(x => x.CalculateCharityDeduction(grossSalary, charity));
            callCalculateCharityDeduction.Returns(charity);
            
            var taxMock = new Mock<ITaxationRule>();
            var nameCall = taxMock.Setup(x => x.Name);
            nameCall.Returns("Mock");
            var callCalculateTax = taxMock.Setup(x => x.CalculateTax(charity));
            callCalculateTax.Returns(charity);

            TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator salaryCalculator = new TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator(charityCalculatorMock.Object, taxMock.Object);

            // Act
            (decimal netSalary, Dictionary<string, decimal> taxes, decimal totalTax) = salaryCalculator.CalculateNetSalary(grossSalary, charity);

            // Assert
            Assert.AreEqual(charity, netSalary);
            Assert.AreEqual(1, taxes.Count);
            Assert.AreEqual("Mock", taxes.Keys.First());
            Assert.AreEqual(charity, taxes.Values.First());
            Assert.AreEqual(charity, totalTax);
            taxMock.Verify(x => x.Name, Times.Once());
            taxMock.Verify(x => x.CalculateTax(charity), Times.Once());
            charityCalculatorMock.Verify(x => x.CalculateCharityDeduction(grossSalary, charity), Times.Once());
        }

        [Test]
        public void WithoutCharitySpent_Should_GrossSalaryNotBeDeducted()
        {
            // Arrange
            var grossSalary = 100;
            var tax = 50;

            var charityCalculatorMock = new Mock<ICharityCalculator>();

            var taxMock = new Mock<ITaxationRule>();
            var nameCall = taxMock.Setup(x => x.Name);
            nameCall.Returns("Mock");
            var callCalculateTax = taxMock.Setup(x => x.CalculateTax(grossSalary));
            callCalculateTax.Returns(tax);

            TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator salaryCalculator = new TaxCalculator.BL.NetSalaryCalculator.NetSalaryCalculator(charityCalculatorMock.Object, taxMock.Object);

            // Act
            (decimal netSalary, Dictionary<string, decimal> taxes, decimal totalTax) = salaryCalculator.CalculateNetSalary(grossSalary, null);

            // Assert
            Assert.AreEqual(tax, netSalary);
            Assert.AreEqual(1, taxes.Count);
            Assert.AreEqual("Mock", taxes.Keys.First());
            Assert.AreEqual(tax, taxes.Values.First());
            Assert.AreEqual(tax, totalTax);
            taxMock.Verify(x => x.Name, Times.Once());
            taxMock.Verify(x => x.CalculateTax(grossSalary), Times.Once());
            charityCalculatorMock.Verify(x => x.CalculateCharityDeduction(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never());
        }
    }
}
