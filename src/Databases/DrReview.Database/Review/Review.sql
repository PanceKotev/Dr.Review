CREATE TABLE [dbo].[Review]
(
	 [ID]               BIGINT                      NOT NULL IDENTITY PRIMARY KEY,
	 [Uid]              UNIQUEIDENTIFIER            NOT NULL UNIQUE,
	 [Suid]             NVARCHAR(200)               NOT NULL UNIQUE,
	 [DeletedOn]        SMALLDATETIME               NULL,
	 [ModifiedOn]       SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
	 [ReviewerFK]		BIGINT						NOT NULL,
	 [RevieweeFK]		BIGINT						NOT NULL,
	 [Comment]			NVARCHAR(1000)				NULL,
	 [Score]			DECIMAL(3,2)				NOT NULL DEFAULT 0,
	 CONSTRAINT [UK_Review_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
	 CONSTRAINT [UK_Review_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn]),
	 CONSTRAINT [FK_Review_Reviewer] FOREIGN KEY([ReviewerFK]) REFERENCES [dbo].[User] ([ID]),
	 CONSTRAINT [FK_Review_Reviewee] FOREIGN KEY([RevieweeFK]) REFERENCES [dbo].[Doctor] ([ID]),
	 CONSTRAINT [Review_Valid_Score] CHECK([Score] >= 0 AND [Score]<=5 AND [Score]%0.5=0)
  ) 
  GO CREATE NONCLUSTERED INDEX [Review_DeletedOn_Uid] 
	ON [dbo].[Review] ([Uid],[DeletedOn])
  GO CREATE NONCLUSTERED INDEX [Review_DeletedOn_Suid] 
	ON [dbo].[Review] ([Suid],[DeletedOn])
