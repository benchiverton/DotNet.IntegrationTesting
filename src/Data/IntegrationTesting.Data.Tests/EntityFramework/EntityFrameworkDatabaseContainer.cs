using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace IntegrationTesting.Data.Tests.EntityFramework;

public class EntityFrameworkDatabaseContainer : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    public Task InitializeAsync() => _msSqlContainer.StartAsync();

    public Task DisposeAsync() => _msSqlContainer.DisposeAsync().AsTask();

    // expose DB context?
}
