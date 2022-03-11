using Microsoft.AspNetCore.Mvc;

using MediatR;
using TaxCalculator.BL.Commands.Calculate;
using System.Threading.Tasks;

namespace TaxCalculator.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private IMediator mediator;

        public CalculatorController(IMediator mediator)
            => this.mediator = mediator;

        [HttpPost("[action]")]
        public async Task<ActionResult<CalculateOutputModel>> Calculate(CalculateCommand calculateCommand)
            => await mediator.Send(calculateCommand);
    }
}
