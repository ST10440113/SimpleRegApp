ALTER TABLE [RegisteredEvents] DROP CONSTRAINT [FK_RegisteredEvents_Events_EventId];

ALTER TABLE [RegisteredEvents] DROP CONSTRAINT [PK_RegisteredEvents];

ALTER TABLE [RegisteredEvents] DROP COLUMN [Id];

ALTER TABLE [RegisteredEvents] ADD [Id] INT IDENTITY(1,1) NOT NULL;

ALTER TABLE [RegisteredEvents] ADD CONSTRAINT [PK_RegisteredEvents] PRIMARY KEY ([Id]);

ALTER TABLE [RegisteredEvents] ADD CONSTRAINT [FK_Registered_Events_Events_EventsId]
FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]) ON DELETE CASCADE;