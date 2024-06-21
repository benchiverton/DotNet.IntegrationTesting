using Xunit;

namespace IntegrationTesting.Data.Tests.Dapper;

[CollectionDefinition("Dapper Database collection")]
public class DapperDatabaseCollection : ICollectionFixture<DapperDatabaseContainer>
{
    // This class has no code, and is never created. Its purpose is
    // to be the place to apply [CollectionDefinition] to all the
    // ICollectionFixture<X> interfaces.
}
