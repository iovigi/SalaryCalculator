namespace SalaryCalculator.Logic.Rules
{
    public interface ITaxationRule
    {
        /// <summary>
        /// Get tax for given salary.
        /// </summary>
        /// <param name="grossSalary">gross salary</param>
        /// <returns>return tax for salary</returns>
        decimal CalculateTax(decimal grossSalary);
    }
}
