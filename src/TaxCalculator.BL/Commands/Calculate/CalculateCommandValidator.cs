using System.Linq;

using FluentValidation;

namespace TaxCalculator.BL.Commands.Calculate
{
    public class CalculateCommandValidator : AbstractValidator<CalculateCommand>
    {
        public CalculateCommandValidator()
        {
            string fullnameRegex = @"^([a-zA-Z]+(\s{1}[a-zA-Z]{1,}){1,})$";

            this.RuleFor(c => c.FullName)
                .Matches(fullnameRegex);

            this.RuleFor(c => c.SSN)
                .MinimumLength(5)
                .MaximumLength(10)
                .Must(c=> c.All(char.IsDigit));

            this.RuleFor(c => c.GrossIncome)
                .GreaterThanOrEqualTo(0);

            this.RuleFor(c => c.CharitySpent)
                .Must(c => !c.HasValue || c.Value >= 0);
        }
    }
}
