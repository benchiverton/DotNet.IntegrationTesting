using System;
using System.Threading.Tasks;
using IntegrationTesting.Repository.DTOs;

namespace IntegrationTesting.Repository;

public interface IBusinessEntityRepository
{
    Task<BusinessEntity> GetBusinessEntity(Guid id);
}

public class BusinessEntityRepository : IBusinessEntityRepository
{
    private readonly string _connectionString;

    public BusinessEntityRepository(string connectionString) => _connectionString = connectionString;

    public Task<BusinessEntity> GetBusinessEntity(Guid id) => throw new NotImplementedException();
}
