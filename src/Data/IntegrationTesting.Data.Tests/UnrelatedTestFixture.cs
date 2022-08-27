using Xunit;

namespace IntegrationTesting.Data.Tests;

public class UnrelatedTestFixture
{
    [Fact]
    public void ShouldPassWithoutDatabaseCreation()
    {
        // this test proves that unit tests not in the 'Database collection' will not wait for DatabaseContainer creation.
        // this test will pass immediately if you run it individually, or if you run all unit tests in this project.
    }
}
