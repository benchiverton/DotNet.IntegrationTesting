using Xunit;

namespace IntegrationTesting.Data.Tests.TestInfrastructure;

[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseContainer>
{
    // This class has no code, and is never created. Its purpose is
    // to be the place to apply [CollectionDefinition] to all the
    // ICollectionFixture<X> interfaces.
}
