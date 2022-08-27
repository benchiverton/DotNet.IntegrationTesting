CREATE PROCEDURE [Domain].[GetBusinessEventsByEntityId]
	@BusinessEntityId UNIQUEIDENTIFIER
AS
	SELECT
        EventId,
        BusinessEntityId,
        EventType,
        EventDetails,
        CreatedUtc
    FROM [Domain].[BusinessEvent]
    WHERE BusinessEntityId = @BusinessEntityId

GO    

GRANT EXECUTE ON [Domain].[GetBusinessEventsByEntityId] TO DomainUser

GO
