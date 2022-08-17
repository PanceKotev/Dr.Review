CREATE TABLE [dbo].[Vote]
(
	 [ID]               BIGINT                      NOT NULL IDENTITY PRIMARY KEY,
	 [Uid]              UNIQUEIDENTIFIER            NOT NULL UNIQUE,
	 [Suid]             NVARCHAR(200)               NOT NULL UNIQUE,
	 [DeletedOn]        SMALLDATETIME               NULL,
	 [ModifiedOn]       SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
	 [ReviewerFK]		BIGINT						NOT NULL,
	 [ReviewFK]			BIGINT						NOT NULL,
	 [Upvoted]			BIT							NULL,
	 CONSTRAINT [UK_Vote_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
	 CONSTRAINT [UK_Vote_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn]),
	 CONSTRAINT [FK_Vote_Reviewer] FOREIGN KEY([ReviewerFK]) REFERENCES [dbo].[User] ([ID]),
	 CONSTRAINT [FK_Vote_Review] FOREIGN KEY([ReviewFK]) REFERENCES [dbo].[Review] ([ID]),
	 CONSTRAINT [Vote_One_Vote_Per_Reviewer_Review] UNIQUE([ReviewerFK], [ReviewFK], [DeletedOn])
  ) 
  GO CREATE NONCLUSTERED INDEX [Vote_DeletedOn_Uid] 
	ON [dbo].[Vote] ([Uid],[DeletedOn])
  GO CREATE NONCLUSTERED INDEX [Vote_DeletedOn_Suid] 
	ON [dbo].[Vote] ([Suid],[DeletedOn])
