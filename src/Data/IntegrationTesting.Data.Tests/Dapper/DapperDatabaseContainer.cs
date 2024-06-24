using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using IntegrationTesting.Data.Tests.TestContainers;
using Microsoft.SqlServer.Dac;
using Xunit;

namespace IntegrationTesting.Data.Tests.Dapper;

public class DapperDatabaseContainer : IAsyncLifetime
{
    private const string Database = "master";
    private const string Username = "sa";
    private const string Password = "$trongPassword";
    private const ushort MsSqlPort = 1433;
    private const string DatabaseName = "ApplicationDatabase";
    private const string DomainLoginPassword = "$trongPassword";

    private IContainer _container;
    private ushort _port;

    public string DomainLoginConnectionString => $"Server=127.0.0.1,{_port};Database={DatabaseName};User Id=DomainLogin;Password={DomainLoginPassword};TrustServerCertificate=True";

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

        _port = _container.GetMappedPublicPort(MsSqlPort);
        var connectionString = $"Server=127.0.0.1,{_port};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True";

        var dbPackageLoc = Environment.GetEnvironmentVariable("DACPAC_LOCATION");
        if (string.IsNullOrEmpty(dbPackageLoc))
        {
            dbPackageLoc = "..\\..\\..\\..\\IntegrationTesting.Data.Sql\\bin\\Debug\\IntegrationTesting.Data.Sql.dacpac";
        }
        var dbPackage = DacPackage.Load(dbPackageLoc);
        var dacServices = new DacServices(connectionString);
        var deployOptions = new DacDeployOptions();
        deployOptions.SqlCommandVariableValues.Add("DomainLoginPassword", DomainLoginPassword);
        dacServices.Deploy(dbPackage, DatabaseName, options: deployOptions);
    }

    public Task DisposeAsync() => _container.StopAsync();
}
