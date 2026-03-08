using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;
using Transparity.Application.Abstractions;
using Transparity.Shared.Exceptions;
using Transparity.Shared.Models;

namespace Transparity.Application.Healths.Queries {
    [ExcludeFromCodeCoverage(Justification = "Health checks related codes should " +
        "not be covered by tests.")]
    public class HealthSummaryQuery : IQuery<Result<HealthReport>> { }

    [ExcludeFromCodeCoverage(Justification = "Health checks related codes should " +
        "not be covered by tests.")]
    public class HealthSummaryQueryHandler : IRequestHandler<HealthSummaryQuery, Result<HealthReport>> {
        private readonly HealthCheckService _healthCheckService;

        public HealthSummaryQueryHandler(HealthCheckService healthCheckService) {
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
