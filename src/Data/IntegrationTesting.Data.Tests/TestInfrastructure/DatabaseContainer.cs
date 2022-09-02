using System;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.SqlServer.Dac;

namespace IntegrationTesting.Data.Tests.TestInfrastructure;

public class DatabaseContainer : IDisposable
{
    private static readonly int HostPort = Random.Shared.Next(49152, 52000);
    private static readonly string DatabaseName = "ApplicationDatabase";
    private static readonly string SaLogin = "sa";
    private static readonly string SaPassword = $"{Random.Shared.Next(100000000, 999999999)}aA!";
    private static readonly string DomainLoginUsername = "DomainLogin";
    private static readonly string DomainLoginPassword = $"{Random.Shared.Next(100000000, 999999999)}aA!";

    private readonly TestcontainerDatabase _testContainer;

    public DatabaseContainer()
    {
        _testContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration("mcr.microsoft.com/mssql/server:2019-CU16-GDR1-ubuntu-20.04")
            {
                Password = SaPassword
            })
            .WithExposedPort("1433")
            .WithPortBinding(HostPort.ToString(), "1433")
            .AddDockerEndpoint()
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .WithCleanUp(true)
            .Build();
        _testContainer.StartAsync().ContinueWith(e =>
        {
            var dbPackageLoc = Environment.GetEnvironmentVariable("DACPAC_LOCATION");
            if (string.IsNullOrEmpty(dbPackageLoc))
            {
                dbPackageLoc = "..\\..\\..\\..\\IntegrationTesting.Data.Sql\\bin\\Debug\\IntegrationTesting.Data.Sql.dacpac";
            }
            var dbPackage = DacPackage.Load(dbPackageLoc);
            var dacServices = new DacServices($"Data Source=127.0.0.1,{HostPort}; User Id={SaLogin}; Password={SaPassword}");
            var deployOptions = new DacDeployOptions();
            deployOptions.SqlCommandVariableValues.Add("DomainLoginPassword", DomainLoginPassword);
            dacServices.Deploy(dbPackage, DatabaseName, options: deployOptions);
        }).GetAwaiter().GetResult();
    }

    public string DomainLoginConnectionString => $"Server=127.0.0.1,{HostPort}; Database={DatabaseName}; User Id={DomainLoginUsername}; Password={DomainLoginPassword}";

    public void Dispose() => _testContainer.StopAsync().GetAwaiter().GetResult();
}
