using System;

namespace TaxCalculator.BL.NetSalaryCalculator.CharityCalculcator
{
    public class CharityCalculator : ICharityCalculator
    {
        private readonly decimal calculatedPercentPerSalary;

        public CharityCalculator(int percentOfGrossSalary)
        => this.calculatedPercentPerSalary = ((decimal)percentOfGrossSalary) / 100;

        public decimal CalculateCharityDeduction(decimal grossSalary, decimal charitySpent)
        => Math.Min(grossSalary * calculatedPercentPerSalary, charitySpent);
    }
}
