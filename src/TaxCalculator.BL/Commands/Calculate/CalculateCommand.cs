using MediatR;

namespace TaxCalculator.BL.Commands.Calculate
{
    public class CalculateCommand : IRequest<CalculateOutputModel>
    {
        public string FullName { get; set; } = default!;
        public string SSN { get; set; } = default!;
        public decimal GrossIncome { get; set; }
        public decimal? CharitySpent { get; set; }
    }
}
