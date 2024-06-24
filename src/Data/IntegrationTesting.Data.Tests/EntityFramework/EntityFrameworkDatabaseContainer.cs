using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using IntegrationTesting.Data.EntityFramework;
using IntegrationTesting.Data.Tests.TestContainers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTesting.Data.Tests.EntityFramework;

public class EntityFrameworkDatabaseContainer : IAsyncLifetime
{
    private const string Database = "master";
    private const string Username = "sa";
    private const string Password = "$trongPassword";
    private const ushort MsSqlPort = 1433;

    private IContainer _container;
    private BusinessEventDbContext _businessEventDbContext;

    public BusinessEventDbContext BusinessEventDbContext => _businessEventDbContext;

    public async Task InitializeAsync()
    {
        _container = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPortBinding(MsSqlPort, true)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("SQLCMDUSER", Username)
            .WithEnvironment("SQLCMDPASSWORD", Password)
            .WithEnvironment("MSSQL_SA_PASSWORD", Password)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MsSqlPort))
            .AddDockerEndpoint()
            .Build();
        await _container.StartAsync();

        var port = _container.GetMappedPublicPort(MsSqlPort);
        var connectionString = $"Server=127.0.0.1,{port};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True";

        var options = new DbContextOptionsBuilder<BusinessEventDbContext>().UseSqlServer(connectionString).Options;
        _businessEventDbContext = new BusinessEventDbContext(options);
        await _businessEventDbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
        await _businessEventDbContext.DisposeAsync();
    }
}
