using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;
using Transparity.Shared.Models;

namespace Transparity.Application.Healths.Checks {
    [ExcludeFromCodeCoverage(Justification = "Health checks related codes should " +
        "not be covered by tests.")]
    public class AppHealthCheck : IHealthCheck {
        private readonly AppState _appState;

        public AppHealthCheck(AppState appState) {
            _appState = appState;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
            CancellationToken cancellationToken = default) {
            if (!_appState.IsReady) {
                return Task.FromResult(HealthCheckResult
                    .Unhealthy("Application is not ready"));
            }

            if (!_appState.IsAlive) {
                return Task.FromResult(HealthCheckResult
                    .Unhealthy("Application is not alive"));
            }

            return Task.FromResult(HealthCheckResult
                .Healthy("Application is running", _appState.ToDictionary()));
        }
    }
}
