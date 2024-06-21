using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using IntegrationTesting.Data.DTOs;
using Microsoft.Data.SqlClient;

namespace IntegrationTesting.Data.Dapper;

public class DapperBusinessEventRepository : IBusinessEventRepository
{
    private readonly string _connectionString;

    public DapperBusinessEventRepository(string connectionString) => _connectionString = connectionString;

    public async Task PersistBusinessEvent(BusinessEvent businessEvent)
    {
        using var sqlConnection = new SqlConnection(_connectionString);

        var parameters = new DynamicParameters();
        parameters.Add("@EventId", businessEvent.EventId);
        parameters.Add("@BusinessEntityId", businessEvent.BusinessEntityId);
        parameters.Add("@EventType", businessEvent.EventType);
        parameters.Add("@EventDetails", businessEvent.EventDetails);
        parameters.Add("@CreatedUtc", businessEvent.CreatedUtc);

        await sqlConnection.ExecuteScalarAsync("[Domain].[InsertBusinessEvent]", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<BusinessEvent>> GetBusinessEventsByBusinessEntityId(Guid businessEntityId)
    {
        using var sqlConnection = new SqlConnection(_connectionString);

        var parameters = new DynamicParameters();
        parameters.Add("@BusinessEntityId", businessEntityId);

        return await sqlConnection.QueryAsync<BusinessEvent>("[Domain].[GetBusinessEventsByEntityId]", parameters, commandType: CommandType.StoredProcedure);
    }
}
