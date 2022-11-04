CREATE TABLE [dbo].[User]
(
	[ID]			BIGINT						NOT NULL PRIMARY KEY IDENTITY,
	[Uid]			UNIQUEIDENTIFIER			NOT NULL UNIQUE,
	[Suid]          NVARCHAR(200)               NOT NULL UNIQUE,
	[DeletedOn]		DATETIME2(7)				NULL,
	[ModifiedOn]	DATETIME2(7)				NOT NULL DEFAULT GETUTCDATE(),
	[FirstName]		NVARCHAR(200)				NOT NULL, 
	[LastName]		NVARCHAR(200)				NOT NULL, 
	[EmailAddress]	NVARCHAR(200)				NULL
)

GO CREATE UNIQUE INDEX [Unique_Email]
	ON [dbo].[User](EmailAddress, DeletedOn)
	WHERE [EmailAddress] IS NOT NULL AND [DeletedOn] IS NULL

GO CREATE NONCLUSTERED INDEX [Index_DeletedOn_UID]
	ON [dbo].[User] ([DeletedOn], [Uid])
GO CREATE NONCLUSTERED INDEX [Index_DeletedOn_Suid]
	ON [dbo].[User] ([DeletedOn], [Suid])
