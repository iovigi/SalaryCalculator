namespace SalaryCalculator.Logic
{
    public interface INetSalaryCalculator
    {
        /// <summary>
        /// Calculate net after tax
        /// </summary>
        /// <param name="grossSalary">gross salary</param>
        /// <returns>return calculated net salary after tax</returns>
        decimal CalculateNetSalary(decimal grossSalary);
    }
}
