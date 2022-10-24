CREATE TABLE [dbo].[Institution]
  (
     [ID]           BIGINT                      NOT NULL PRIMARY KEY,
     [Uid]          UNIQUEIDENTIFIER            NOT NULL UNIQUE,
     [Suid]         NVARCHAR(200)               NOT NULL UNIQUE,
     [DeletedOn]    DATETIME2(7)                NULL,
     [ModifiedOn]   SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
     [Name]         NVARCHAR(MAX)               NOT NULL,
     [LocationFK]   BIGINT                      NOT NULL,
     CONSTRAINT [Institution_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
     CONSTRAINT [Institution_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn]),
     CONSTRAINT [FK_Institution_Location] FOREIGN KEY([LocationFK]) REFERENCES [dbo].[Location] ([ID])
  ) 
  GO CREATE NONCLUSTERED INDEX [Institution_DeletedOn_Uid] 
    ON [dbo].[Institution] ([Uid],[DeletedOn])
  GO CREATE NONCLUSTERED INDEX [Institution_DeletedOn_Suid] 
    ON [dbo].[Institution] ([Suid],[DeletedOn])
