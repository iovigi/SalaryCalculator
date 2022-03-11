namespace TaxCalculator.BL.NetSalaryCalculator.TaxRules
{
    public interface ITaxationRule
    {
        /// <summary>
        /// Name of the taxs
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get tax for given salary.
        /// </summary>
        /// <param name="grossSalary">gross salary</param>
        /// <returns>return tax for salary</returns>
        decimal CalculateTax(decimal grossSalary);
    }
}
