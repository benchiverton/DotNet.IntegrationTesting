using System.Threading.Tasks;
using IntegrationTesting.Repository.DTOs;

namespace IntegrationTesting.Repository;

public interface IBusinessEventRepository
{
    Task PersistBusinessEvent(BusinessEvent businessEvent);
}

public class BusinessEventRepository : IBusinessEventRepository
{
    private readonly string _connectionString;

    public BusinessEventRepository(string connectionString) => _connectionString = connectionString;

    public Task PersistBusinessEvent(BusinessEvent businessEvent) => throw new System.NotImplementedException();
}
