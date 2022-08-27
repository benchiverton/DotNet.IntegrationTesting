CREATE PROCEDURE [Domain].[InsertBusinessEvent]
	@EventId UNIQUEIDENTIFIER,
	@BusinessEntityId UNIQUEIDENTIFIER,
    @EventType VARCHAR(50),
    @EventDetails NVARCHAR(MAX),
    @CreatedUtc DATETIME2
AS
	INSERT INTO [Domain].[BusinessEvent] (
        EventId,
        BusinessEntityId,
        EventType,
        EventDetails,
        CreatedUtc
    ) VALUES (
        @EventId,
        @BusinessEntityId,
        @EventType,
        @EventDetails,
        @CreatedUtc
    )
GO

GRANT EXECUTE ON [Domain].[InsertBusinessEvent] TO DomainUser

GO
