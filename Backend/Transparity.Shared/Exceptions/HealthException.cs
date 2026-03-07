using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace Transparity.Shared.Exceptions {
    public class HealthException : Exception {
        public HealthException(string? message) : base(message) { }
        public HealthException(string? message, Exception innerException)
            : base(message, innerException) { }

        public static void ThrowIfNotHealthy(HealthReport report) {
            if (report.Status is not HealthStatus.Healthy) {
                var message = $"Health check reported an {report.Status} status";
                var innerException = new Exception(JsonSerializer.Serialize(report));

                throw new HealthException(message, innerException);
            }
        }
    }
}
