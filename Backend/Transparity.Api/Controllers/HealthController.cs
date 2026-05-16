using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Transparity.Application.Abstractions;
using Transparity.Application.Healths.Queries;
using Transparity.Shared.Models;

namespace Transparity.Api.Controllers {
    [Route("api/health")]
    public class HealthController : BaseController {
        private readonly IMediator _mediator;

        public HealthController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result<HealthReport>> Summary([FromQuery] HealthSummaryQuery query) {
            return await _mediator.SendAsync(query);
        }
    }
}
