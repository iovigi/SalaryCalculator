namespace TaxCalculator.BL.NetSalaryCalculator.CharityCalculcator
{
    public interface ICharityCalculator
    {
        /// <summary>
        /// Calculate charity which should be deduct from gross salary
        /// </summary>
        /// <param name="grossSalary">gross salary</param>
        /// <param name="charitySpent">charity spent</param>
        /// <returns>return charity deduction</returns>
        decimal CalculateCharityDeduction(decimal grossSalary, decimal charitySpent);
    }
}
