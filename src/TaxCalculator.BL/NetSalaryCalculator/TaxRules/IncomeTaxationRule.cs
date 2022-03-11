using System;

namespace TaxCalculator.BL.NetSalaryCalculator.TaxRules
{
    public class IncomeTaxationRule : ITaxationRule
    {
        private readonly decimal belowTaxSalaryLevel;
        private readonly decimal calculatedPercentPerSalary;

        public IncomeTaxationRule(decimal belowTaxSalaryLevel, int percentPerSalary)
        {
            if (belowTaxSalaryLevel < 0)
            {
                throw new ArgumentException("below tax salary level can't be lower than 0");
            }

            if (percentPerSalary < 0 || percentPerSalary > 100)
            {
                throw new ArgumentException("percent per slary can't be lower than 0 and higher than 100");
            }

            this.belowTaxSalaryLevel = belowTaxSalaryLevel;
            this.calculatedPercentPerSalary = ((decimal)percentPerSalary) / 100;
        }

        public string Name => "IncomeTax";

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

            decimal tax = (grossSalary - this.belowTaxSalaryLevel) * this.calculatedPercentPerSalary;

            return tax;
        }
    }
}
