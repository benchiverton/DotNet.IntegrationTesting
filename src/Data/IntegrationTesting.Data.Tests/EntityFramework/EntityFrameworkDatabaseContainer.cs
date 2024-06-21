using System.Threading.Tasks;
using IntegrationTesting.Data.EntityFramework;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Xunit;

namespace IntegrationTesting.Data.Tests.EntityFramework;

public class EntityFrameworkDatabaseContainer : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        var connection = new SqlConnection(_msSqlContainer.GetConnectionString());
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<BusinessEventDbContext>().UseSqlServer(connection).Options;
        _businessEventDbContext = new BusinessEventDbContext(options);
        await _businessEventDbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        await _businessEventDbContext.DisposeAsync();
    }

    public BusinessEventDbContext _businessEventDbContext;
}
