using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Transparity.Data;

namespace Transparity.Tests.Integration.Fixtures {
    public class PostgresFixture : IAsyncLifetime {
        private PostgreSqlContainer _postgres = default!;
        public string ConnectionString => _postgres.GetConnectionString();

        public async Task InitializeAsync() {
            _postgres = new PostgreSqlBuilder("postgres:17-alpine")
                .WithDatabase("neon_test")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .Build();

            await _postgres.StartAsync();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(_postgres.GetConnectionString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            await dbContext.Database
                .MigrateAsync();
        }

        public async Task DisposeAsync() {
            await _postgres.DisposeAsync();
        }
    }
}
