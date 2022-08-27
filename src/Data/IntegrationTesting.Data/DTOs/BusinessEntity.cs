using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTesting.Data.DTOs;

public class BusinessEntity
{
    private readonly IEnumerable<BusinessEvent> _events;

    public BusinessEntity(IEnumerable<BusinessEvent> events)
    {
        _events = events;

        var latestEvent = _events
            .OrderBy(e => e.CreatedUtc)
            .Last();
        CurrentStatus = latestEvent.EventType;
        LastUpUpdated = latestEvent.CreatedUtc;
    }

    public string CurrentStatus { get; }
    public DateTime LastUpUpdated { get; }
}
