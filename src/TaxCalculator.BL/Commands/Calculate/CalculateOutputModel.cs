namespace TaxCalculator.BL.Commands.Calculate
{
    public class CalculateOutputModel
    {
        public decimal GrossIncome { get; set; }
        public decimal? CharitySpent { get; set; }
        
        //TODO: In the task, it is said that this is the way the contract should be, this criple the contract. If we can change the condition in the task to return dictionary with every taxes, this will bring more flexibility.
        public decimal IncomeTax { get; set; }
        public decimal SocialTax { get; set; }

        public decimal TotalTax { get; set; }
        public decimal NetIncome { get; set; }
    }
}
