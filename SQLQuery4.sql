CREATE TABLE RegisteredEvents (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    FName NVARCHAR(255) NULL,
    LName NVARCHAR(255) NULL,

    EmailAddress NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,

    EventId INT NOT NULL,

    CONSTRAINT FK_RegisteredEvents_Events
        FOREIGN KEY (EventId) REFERENCES Events(EventId)
        ON DELETE CASCADE
);