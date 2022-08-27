using System;

namespace IntegrationTesting.Data.DTOs;

public record BusinessEvent(
    Guid EventId,
    Guid BusinessEntityId,
    string EventType,
    string EventDetails,
    DateTime CreatedUtc
);
