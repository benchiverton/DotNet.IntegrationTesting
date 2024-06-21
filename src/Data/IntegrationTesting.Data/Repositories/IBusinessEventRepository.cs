using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationTesting.Data.DTOs;

namespace IntegrationTesting.Data.Repositories;

public interface IBusinessEventRepository
{
    Task PersistBusinessEvent(BusinessEvent businessEvent);
    Task<IEnumerable<BusinessEvent>> GetBusinessEventsByBusinessEntityId(Guid businessEntityId);
}
