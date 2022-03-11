using System.Collections.Generic;

namespace TaxCalculator.BL.NetSalaryCalculator
{
    public interface INetSalaryCalculator
    {
        /// <summary>
        /// Calculate net after tax
        /// </summary>
        /// <param name="grossSalary">gross salary</param>
        /// <param name="charitySpent">charity spent</param>
        /// <returns>return net salary, taxes and totalTax</returns>
        (decimal netSalary, Dictionary<string, decimal> taxes, decimal totalTax) CalculateNetSalary(decimal grossSalary, decimal? charitySpent);
    }
}
