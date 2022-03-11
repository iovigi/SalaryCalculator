using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.BL.Commands.Calculate;
using TaxCalculator.BL.NetSalaryCalculator;

namespace TaxCalculator.BL.Tests.Commands.Calculate
{
    delegate void CacheCallback(object key, out CalculateOutputModel value);

    public class CalculateCommandHandlerTests
    {
        [Test]
        public async Task WhenExistInCache_Should_Return()
        {
            // Arrange
            var netSalaryCalculatorMock = new Mock<INetSalaryCalculator>();
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var request = new CalculateCommand();

            var calculateOutputModel = new CalculateOutputModel();
            calculateOutputModel.NetIncome = 100;
            memoryCache.Set((request.GrossIncome, request.CharitySpent), calculateOutputModel);

            CalculateCommandHandler handler = new CalculateCommandHandler(netSalaryCalculatorMock.Object, memoryCache);

            // Act
            var result = await handler.Handle(request, default);

            // Assert
            Assert.AreEqual(calculateOutputModel.NetIncome, result.NetIncome);
            netSalaryCalculatorMock.Verify(x => x.CalculateNetSalary(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never());
        }

        [Test]
        public async Task WhenNotExistInCache_Should_Calculate()
        {
            // Arrange
            var netSalaryCalculatorMock = new Mock<INetSalaryCalculator>();
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var request = new CalculateCommand();

            var calculateOutputModel = new CalculateOutputModel();
            calculateOutputModel.NetIncome = 100;

            Dictionary<string, decimal> taxes = new Dictionary<string, decimal>();
            taxes["IncomeTax"] = calculateOutputModel.IncomeTax;
            taxes["SocialTax"] = calculateOutputModel.SocialTax;

            netSalaryCalculatorMock.Setup(x => x.CalculateNetSalary(It.IsAny<decimal>(), It.IsAny<decimal?>()))
                .Returns((calculateOutputModel.NetIncome, taxes, calculateOutputModel.TotalTax));

            CalculateCommandHandler handler = new CalculateCommandHandler(netSalaryCalculatorMock.Object, memoryCache);

            // Act
            var result = await handler.Handle(request, default);

            // Assert
            Assert.AreEqual(calculateOutputModel.NetIncome, result.NetIncome);
            Assert.AreEqual(calculateOutputModel.IncomeTax, result.IncomeTax);
            Assert.AreEqual(calculateOutputModel.SocialTax, result.SocialTax);
            Assert.AreEqual(calculateOutputModel.TotalTax, result.TotalTax);
            Assert.AreEqual(calculateOutputModel.CharitySpent, result.CharitySpent);
            netSalaryCalculatorMock.Verify(x => x.CalculateNetSalary(request.GrossIncome, request.CharitySpent), Times.Once());
        }
    }
}
