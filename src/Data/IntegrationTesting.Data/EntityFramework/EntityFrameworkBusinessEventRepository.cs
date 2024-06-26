﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationTesting.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTesting.Data.EntityFramework;

public class EntityFrameworkBusinessEventRepository : IBusinessEventRepository
{
    private readonly BusinessEventDbContext _businessEventDbContext;

    public EntityFrameworkBusinessEventRepository(BusinessEventDbContext businessEventDbContext) =>
        _businessEventDbContext = businessEventDbContext;

    public async Task PersistBusinessEvent(BusinessEvent businessEvent)
    {
        await _businessEventDbContext.AddAsync(businessEvent);
        await _businessEventDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BusinessEvent>> GetBusinessEventsByBusinessEntityId(Guid businessEntityId) =>
        await _businessEventDbContext.BusinessEvents.Where(be => be.BusinessEntityId == businessEntityId)
            .ToListAsync();
}
