CREATE TABLE [dbo].[Specialization]
  (
     [ID]           BIGINT                      NOT NULL IDENTITY PRIMARY KEY,
     [Uid]          UNIQUEIDENTIFIER            NOT NULL UNIQUE,
     [Suid]         NVARCHAR(200)               NOT NULL UNIQUE,
     [DeletedOn]    SMALLDATETIME               NULL,
     [ModifiedOn]   SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
     [Name]         NVARCHAR(MAX)               NOT NULL,
     CONSTRAINT [Specialization_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
     CONSTRAINT [Specialization_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn])
  ) 
 GO CREATE NONCLUSTERED INDEX [Specialization_DeletedOn_Uid] 
    ON [dbo].[Specialization] ([Uid],[DeletedOn])
 GO CREATE NONCLUSTERED INDEX [Specialization_DeletedOn_Suid] 
    ON [dbo].[Specialization] ([Suid],[DeletedOn])