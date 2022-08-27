using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.SqlServer.Dac;

namespace IntegrationTesting.Repository.Tests.TestInfrastructure;

public class DatabaseContainerFactory : IAsyncDisposable
{
    private readonly TestcontainerDatabase _testContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
        .WithPortBinding(1433, true)
        .WithDatabase(new MsSqlTestcontainerConfiguration
        {
            Password = Random.Shared.Next().ToString()
        })
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
        .Build();
    private readonly string _domainLoginUsername = "DomainLogin";
    private readonly string _domainLoginPassword = Random.Shared.Next().ToString();

    public string ConnectionString => $"Server=localhost; Database=master; User Id={_domainLoginUsername}; Password={_domainLoginPassword}";

    public Task InitializeAsync() => _testContainer.StartAsync().ContinueWith(e =>
    {
        var dbPackage = DacPackage.Load("..\\..\\..\\..\\IntegrationTesting.Database\\bin\\Debug\\IntegrationTesting.Database.dacpac");
        var dacServices = new DacServices(_testContainer.ConnectionString);
        var publishOptions = new PublishOptions();
        publishOptions.DeployOptions.SqlCommandVariableValues.Add("DomainLoginPassword", _domainLoginPassword);
        dacServices.Publish(dbPackage, "master", publishOptions);
    });

    public async ValueTask DisposeAsync() => await _testContainer.StopAsync();
}
