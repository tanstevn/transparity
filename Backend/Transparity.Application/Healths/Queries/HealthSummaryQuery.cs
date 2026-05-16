using Microsoft.Extensions.Diagnostics.HealthChecks;
using Transparity.Application.Abstractions;
using Transparity.Shared.Exceptions;
using Transparity.Shared.Models;

namespace Transparity.Application.Healths.Queries {
    public class HealthSummaryQuery : IQuery<Result<HealthReport>> { }

    public class HealthSummaryQueryHandler : IRequestHandler<HealthSummaryQuery, Result<HealthReport>> {
        private readonly HealthCheckService _healthCheckService;

        public HealthSummaryQueryHandler(HealthCheckService healthCheckService) {
            _healthCheckService = healthCheckService;
        }

        public async Task<Result<HealthReport>> HandleAsync(HealthSummaryQuery request) {
            var report = await _healthCheckService.CheckHealthAsync();
            NotFoundException.ThrowIfNull(report, nameof(report));

            if (report.Status is not HealthStatus.Healthy) {
                return Result<HealthReport>
                    .Error($"Health check reported an {report.Status} status", report);
            }

            return Result<HealthReport>
                .Success(report);
        }
    }
}
