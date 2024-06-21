using System;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTesting.Data.Dapper;
using IntegrationTesting.Data.DTOs;
using IntegrationTesting.Data.Tests.Dapper;
using Xunit;

namespace IntegrationTesting.Data.Tests.EntityFramework;

[Collection("Entity Framework Database collection")]
public class EntityFrameworkBusinessEventRepositoryShould
{
    private readonly EntityFrameworkDatabaseContainer _efDatabase;

    public EntityFrameworkBusinessEventRepositoryShould(EntityFrameworkDatabaseContainer efDatabase) => _efDatabase = efDatabase;

    [Fact]
    public async Task PersistAndGetsRelatedBusinessEvents()
    {
    }

    [Fact]
    public async Task PersistAndNotGetUnrelatedBusinessEvents()
    {
    }
}
