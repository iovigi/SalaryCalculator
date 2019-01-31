namespace SalaryCalculator.ConsoleApp
{
    using System;

    using SalaryCalculator.Logic;
    using SalaryCalculator.Logic.Rules;

    public class StartUp
    {
        static void Main(string[] args)
        {
            decimal grossSalary = 0m;

            Console.WriteLine("Please input gross salary. Gross salary should be number between 0 to {0}", decimal.MaxValue);
            string userInput = Console.ReadLine();

            while (!decimal.TryParse(userInput, out grossSalary) || grossSalary < 0)
            {
                Console.WriteLine("Please input valid gross salary. Valid gross salary should be number between 0 to {0}", decimal.MaxValue);
                userInput = Console.ReadLine();
            }

            const decimal belowTaxSalaryLevel = 1000;
            const decimal higherSalaryLevel = 3000;
            const int percentOfIncomeTaxation = 10;
            const int percentOfSocialContributionTaxation = 15;

            var rules = new ITaxationRule[]
            {
                new IncomeTaxationRule(belowTaxSalaryLevel,percentOfIncomeTaxation),
                new SocialContributionTaxationRule(belowTaxSalaryLevel,higherSalaryLevel,percentOfSocialContributionTaxation)
            };

            NetSalaryCalculator salaryCalculator = new NetSalaryCalculator(rules);
            var netSalary = salaryCalculator.CalculateNetSalary(grossSalary);

            Console.WriteLine("Net salary after taxes is {0}", netSalary);
        }
    }
}
