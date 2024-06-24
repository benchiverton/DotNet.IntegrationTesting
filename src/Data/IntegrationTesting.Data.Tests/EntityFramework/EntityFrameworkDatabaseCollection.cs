using Xunit;

namespace IntegrationTesting.Data.Tests.EntityFramework;

[CollectionDefinition("Entity Framework Database collection")]
public class EntityFrameworkDatabaseCollection : ICollectionFixture<EntityFrameworkDatabaseContainer>
{
    // This class has no code, and is never created. Its purpose is
    // to be the place to apply [CollectionDefinition] to all the
    // ICollectionFixture<X> interfaces.
}
