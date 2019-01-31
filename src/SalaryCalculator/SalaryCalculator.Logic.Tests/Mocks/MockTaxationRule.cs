namespace SalaryCalculator.Logic.Tests.Mocks
{
    using Logic.Rules;

    public class MockTaxationRule : ITaxationRule
    {
        private const decimal dummyPercent = 0.1m;

        public decimal CalculateTax(decimal grossSalary)
        {
            return grossSalary * dummyPercent;
        }
    }
}
