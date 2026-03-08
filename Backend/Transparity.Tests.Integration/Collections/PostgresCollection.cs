using Transparity.Tests.Integration.Fixtures;

namespace Transparity.Tests.Integration.Collections {
    [CollectionDefinition("Postgres")]
    public class PostgresCollection : ICollectionFixture<PostgresFixture> { }
}
