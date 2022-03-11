using System;

namespace TaxCalculator.BL.NetSalaryCalculator.TaxRules
{
    public class SocialContributionTaxationRule : ITaxationRule
    {
        private readonly decimal belowTaxSalaryLevel;
        private readonly decimal higherSalaryLevel;
        private readonly decimal calculatedPercentPerSalary;

        public SocialContributionTaxationRule(decimal belowTaxSalaryLevel, decimal higherSalaryLevel, int percentPerSalary)
        {
            if (belowTaxSalaryLevel < 0)
            {
                throw new ArgumentException("below tax salary level can't be lower than 0");
            }

            if (higherSalaryLevel < 0)
            {
                throw new ArgumentException("higher salary level can't be lower than 0");
            }

            if (belowTaxSalaryLevel > higherSalaryLevel)
            {
                throw new ArgumentException("higher salary level can't be lower than below tax salary level");
            }

            if (percentPerSalary < 0 || percentPerSalary > 100)
            {
                throw new ArgumentException("percent per slary can't be lower than 0 and higher than 100");
            }

            this.belowTaxSalaryLevel = belowTaxSalaryLevel;
            this.higherSalaryLevel = higherSalaryLevel;
            this.calculatedPercentPerSalary = ((decimal)percentPerSalary) / 100;
        }

        public string Name => "SocialTax";

        public decimal CalculateTax(decimal grossSalary)
        {
            if (grossSalary < 0)
            {
                throw new ArgumentException("salary can't be lower than 0");
            }

            if (grossSalary <= this.belowTaxSalaryLevel)
            {
                return 0m;
            }

            decimal taxableSalary = grossSalary;

            if (taxableSalary > this.higherSalaryLevel)
            {
                taxableSalary = this.higherSalaryLevel;
            }

            decimal tax = (taxableSalary - this.belowTaxSalaryLevel) * this.calculatedPercentPerSalary;

            return tax;
        }
    }
}
