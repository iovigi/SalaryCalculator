namespace SalaryCalculator.Logic
{
    using System;

    using Rules;

    public class NetSalaryCalculator : INetSalaryCalculator
    {
        private ITaxationRule[] taxationRules;

        public NetSalaryCalculator(params ITaxationRule[] taxationRules)
        {
            this.taxationRules = taxationRules;
        }


        public decimal CalculateNetSalary(decimal grossSalary)
        {
            if(grossSalary < 0)
            {
                throw new ArgumentException("salary can't be lower than 0");
            }

            if(this.taxationRules == null || this.taxationRules.Length == 0)
            {
                return grossSalary;
            }

            decimal taxSum = 0m;

            foreach(var rule in this.taxationRules)
            {
                taxSum += rule.CalculateTax(grossSalary);
            }

            return grossSalary - taxSum;
        }
    }
}
