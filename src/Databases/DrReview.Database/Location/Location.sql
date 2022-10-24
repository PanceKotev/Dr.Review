CREATE TABLE [dbo].[Location]
  (
	 [ID]           BIGINT                      NOT NULL PRIMARY KEY,
	 [Uid]          UNIQUEIDENTIFIER            NOT NULL UNIQUE,
	 [Suid]         NVARCHAR(200)               NOT NULL UNIQUE,
	 [DeletedOn]    DATETIME2(7)	            NULL,
	 [ModifiedOn]   SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
	 [Name]         NVARCHAR(MAX)               NOT NULL,
	 [Longitude]    DECIMAL(19, 16)             NOT NULL,
	 [Latitude]     DECIMAL(18, 16)             NOT NULL,
	 CONSTRAINT [Location_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
	 CONSTRAINT [Location_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn])
  ) 
  GO CREATE NONCLUSTERED INDEX [Location_DeletedOn_Uid] 
	ON [dbo].[Location] ([Uid],[DeletedOn])
 GO CREATE NONCLUSTERED INDEX [Location_DeletedOn_Suid] 
	ON [dbo].[Location] ([Suid],[DeletedOn])