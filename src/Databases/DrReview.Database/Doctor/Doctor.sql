CREATE TABLE [dbo].[Doctor]
  (
     [ID]           BIGINT                      NOT NULL PRIMARY KEY,
     [Uid]          UNIQUEIDENTIFIER            NOT NULL UNIQUE,
     [DeletedOn]    SMALLDATETIME               NULL,
     [ModifiedOn]   SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
     [FirstName]    NVARCHAR(500)               NOT NULL,
     [LastName]     NVARCHAR(500)               NOT NULL,
     [Occupation]   NVARCHAR(500)               NOT NULL,
     [OrdinationFK] BIGINT                      NOT NULL,
     CONSTRAINT [Uid_DeletedOn] UNIQUE([Uid], [DeletedOn])
  ) 
  GO 
    CREATE NONCLUSTERED INDEX [Doctor_DeletedOn_Uid] 
    ON [dbo].[Doctor] ([Uid],[DeletedOn])