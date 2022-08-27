using System;

namespace IntegrationTesting.Repository.DTOs;

public record BusinessEvent(
    Guid EventId,
    Guid BusinessEntityId,
    string EventType,
    string EventDetails,
    DateTime CreatedUtc
);
