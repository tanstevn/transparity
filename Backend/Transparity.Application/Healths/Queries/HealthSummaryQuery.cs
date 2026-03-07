using Microsoft.Extensions.Diagnostics.HealthChecks;
using Transparity.Application.Abstractions;
using Transparity.Shared.Exceptions;
using Transparity.Shared.Models;

namespace Transparity.Application.Healths.Queries {
    public class HealthSummaryQuery : IQuery<Result<HealthReport>> { }

    public class HealthReadyQueryHandler : IRequestHandler<HealthSummaryQuery, Result<HealthReport>> {
        private readonly HealthCheckService _healthCheckService;

        public HealthReadyQueryHandler(HealthCheckService healthCheckService) {
            _healthCheckService = healthCheckService;
        }

        public async Task<Result<HealthReport>> HandleAsync(HealthSummaryQuery request) {
            var report = await _healthCheckService.CheckHealthAsync();

            HealthException.ThrowIfNotHealthy(report);

            return Result<HealthReport>
                .Success(report);
        }
    }
}
