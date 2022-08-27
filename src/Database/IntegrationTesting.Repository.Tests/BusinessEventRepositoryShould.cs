using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Configurations;
using IntegrationTesting.Repository.DTOs;
using IntegrationTesting.Repository.Tests.TestInfrastructure;
using Xunit;

namespace IntegrationTesting.Repository.Tests;

public class BusinessEventRepositoryShould
{
    [Fact]
    public async Task PersistBusinessEventSuccessfully()
    {
        var db = new DatabaseContainerFactory();
        await db.InitializeAsync();

        var repository = new BusinessEventRepository(db.ConnectionString);
        var businessEvent = new BusinessEvent(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "TestEvent",
            "{ \"Key\": \"Value\" }",
            DateTime.UtcNow);

        await repository.PersistBusinessEvent(businessEvent);
    }
}
