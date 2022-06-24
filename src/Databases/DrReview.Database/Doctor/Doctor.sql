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
	 [FullTextSearch]   NVARCHAR(2000)              NULL,
	 CONSTRAINT [Doctor_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
	 CONSTRAINT [Doctor_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn]),
	 CONSTRAINT [FK_Doctor_Specialization] FOREIGN KEY([SpecializationFK]) REFERENCES [dbo].[Specialization] ([ID]),
	 CONSTRAINT [FK_Doctor_Institution] FOREIGN KEY([InstitutionFK]) REFERENCES [dbo].[Institution] ([ID])

  ) 
  GO CREATE NONCLUSTERED INDEX [Doctor_DeletedOn_Uid] 
	ON [dbo].[Doctor] ([Uid],[DeletedOn])
  GO CREATE NONCLUSTERED INDEX [Doctor_DeletedOn_Suid] 
	ON [dbo].[Doctor] ([Suid],[DeletedOn])
  GO CREATE UNIQUE INDEX [Doctor_PK_ID] ON [dbo].[Doctor]([ID])

	
GO CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;
GO CREATE FULLTEXT INDEX ON [dbo].[Doctor]([FullTextSearch]) KEY INDEX [Doctor_PK_ID];

GO CREATE TRIGGER [dbo].[Trigger_Doctor_FullTextSearch]
ON [dbo].[Doctor]
AFTER INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON
	IF (UPDATE([FirstName])
	or UPDATE([LastName]))
	BEGIN
		UPDATE [dbo].[Doctor]
		SET FullTextSearch =
		TRIM(COALESCE(D.FirstName + ' ', '') +
			 COALESCE(D.LastName + ' ', '') +
			 COALESCE(STUFF((SELECT ' ' + I.Name
							 FROM [dbo].[Institution] as I
							 WHERE inserted.InstitutionFK = I.ID AND I.DeletedOn IS NULL
							 FOR XML PATH('')), 
							 1, 1, '') + ' ', '')) + ' ' +
			 COALESCE(STUFF((SELECT ' ' + S.Name
							 FROM [dbo].[Specialization] as S
							 WHERE inserted.SpecializationFK = S.ID AND S.DeletedOn IS NULL
							 FOR XML PATH('')), 
							 1, 1, '') + ' ', '')

		FROM Doctor D INNER JOIN inserted ON inserted.ID = D.ID
		WHERE
			D.DeletedOn IS NULL
	END
END
