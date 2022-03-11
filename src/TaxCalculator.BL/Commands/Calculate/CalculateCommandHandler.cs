using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TaxCalculator.BL.NetSalaryCalculator;

namespace TaxCalculator.BL.Commands.Calculate
{
    public class CalculateCommandHandler : IRequestHandler<CalculateCommand, CalculateOutputModel>
    {
        private readonly INetSalaryCalculator netSalaryCalculator;
        private readonly IMemoryCache memoryCache;

        public CalculateCommandHandler(INetSalaryCalculator netSalaryCalculator, IMemoryCache memoryCache)
        {
            this.netSalaryCalculator = netSalaryCalculator;
            this.memoryCache = memoryCache;
        }

        public async Task<CalculateOutputModel> Handle(CalculateCommand request, CancellationToken cancellationToken)
        {
            if(memoryCache.TryGetValue((request.GrossIncome, request.CharitySpent), out CalculateOutputModel result))
            {
                return result;
            }

            (decimal netIncome, Dictionary<string, decimal> taxes, decimal totalTax) = netSalaryCalculator.CalculateNetSalary(request.GrossIncome, request.CharitySpent);
            
            result = new CalculateOutputModel();
            result.GrossIncome = request.GrossIncome;
            result.CharitySpent = request.CharitySpent;
            result.NetIncome = netIncome;

            //TODO: In the task, it is said that this is the way the contract should be, this criple the contract. If we can change the condition in the task to return dictionary with every taxes, this will bring more flexibility.
            result.IncomeTax = taxes[nameof(result.IncomeTax)];
            result.SocialTax = taxes[nameof(result.SocialTax)];
            
            result.TotalTax = totalTax;

            memoryCache.Set((request.GrossIncome, request.CharitySpent), result);

            return result;
        }
    }
}
