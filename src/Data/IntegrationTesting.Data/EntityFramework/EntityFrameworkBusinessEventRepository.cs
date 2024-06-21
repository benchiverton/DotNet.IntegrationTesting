using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationTesting.Data.DTOs;

namespace IntegrationTesting.Data.EntityFramework;

public class EntityFrameworkBusinessEventRepository : IBusinessEventRepository
{
    public Task PersistBusinessEvent(BusinessEvent businessEvent)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BusinessEvent>> GetBusinessEventsByBusinessEntityId(Guid businessEntityId)
    {
        throw new NotImplementedException();
    }
}
