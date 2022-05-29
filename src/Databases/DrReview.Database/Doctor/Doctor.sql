CREATE TABLE [dbo].[Doctor]
  (
     [ID]               BIGINT                      NOT NULL PRIMARY KEY,
     [Uid]              UNIQUEIDENTIFIER            NOT NULL UNIQUE,
     [Suid]             NVARCHAR(200)               NOT NULL UNIQUE,
     [DeletedOn]        SMALLDATETIME               NULL,
     [ModifiedOn]       SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
     [FirstName]        NVARCHAR(500)               NOT NULL,
     [LastName]         NVARCHAR(500)               NOT NULL,
     [SpecializationFK] BIGINT                      NOT NULL,
     [InstitutionFK]    BIGINT                      NOT NULL,
     CONSTRAINT [Doctor_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
     CONSTRAINT [Doctor_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn]),
     CONSTRAINT [FK_Doctor_Specialization] FOREIGN KEY([SpecializationFK]) REFERENCES [dbo].[Specialization] ([ID]),
     CONSTRAINT [FK_Doctor_Institution] FOREIGN KEY([InstitutionFK]) REFERENCES [dbo].[Institution] ([ID])

  ) 
  GO CREATE NONCLUSTERED INDEX [Doctor_DeletedOn_Uid] 
    ON [dbo].[Doctor] ([Uid],[DeletedOn])
  GO CREATE NONCLUSTERED INDEX [Doctor_DeletedOn_Suid] 
    ON [dbo].[Doctor] ([Suid],[DeletedOn])
