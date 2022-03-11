using System;
using System.Collections.Generic;

using TaxCalculator.BL.NetSalaryCalculator.CharityCalculcator;
using TaxCalculator.BL.NetSalaryCalculator.TaxRules;

namespace TaxCalculator.BL.NetSalaryCalculator
{
    public class NetSalaryCalculator : INetSalaryCalculator
    {
        private ICharityCalculator charityCalculator;
        private ITaxationRule[] taxationRules;

        public NetSalaryCalculator(ICharityCalculator charityCalculator, params ITaxationRule[] taxationRules)
        {
            this.charityCalculator = charityCalculator;
            this.taxationRules = taxationRules;
        }


        public (decimal netSalary, Dictionary<string, decimal> taxes, decimal totalTax) CalculateNetSalary(decimal grossSalary, decimal? charitySpent)
        {
            if (grossSalary < 0)
            {
                throw new ArgumentException("salary can't be lower than 0");
            }

            if (charitySpent < 0)
            {
                throw new ArgumentException("charoty spent can't be lower than 0");
            }

            Dictionary<string, decimal> taxes = new Dictionary<string, decimal>();

            if (this.taxationRules == null || this.taxationRules.Length == 0)
            {
                return (grossSalary, taxes, 0);
            }

            var taxableSalary = charitySpent.HasValue ?
                grossSalary - this.charityCalculator.CalculateCharityDeduction(grossSalary, charitySpent.Value) :
                grossSalary;

            decimal taxSum = 0m;

            foreach (var rule in this.taxationRules)
            {
                var tax = rule.CalculateTax(taxableSalary);
                taxes[rule.Name] = tax;

                taxSum += tax;
            }

            return (grossSalary - taxSum, taxes, taxSum);
        }
    }
}
