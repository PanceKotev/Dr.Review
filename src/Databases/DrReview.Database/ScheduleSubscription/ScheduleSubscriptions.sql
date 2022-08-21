CREATE TABLE [dbo].[ScheduleSubscription]
(
	 [ID]               BIGINT                      NOT NULL IDENTITY PRIMARY KEY,
	 [Uid]              UNIQUEIDENTIFIER            NOT NULL UNIQUE,
	 [Suid]             NVARCHAR(200)               NOT NULL UNIQUE,
	 [DeletedOn]        SMALLDATETIME               NULL,
	 [ModifiedOn]       SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
	 [DoctorFK]			BIGINT						NOT NULL,
	 [UserFK]			BIGINT						NOT NULL,
	 [RangeFrom]		DATE						NOT NULL,
	 [RangeTo]			DATE						NOT NULL,
	 CONSTRAINT [UK_ScheduleSubscription_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
	 CONSTRAINT [UK_ScheduleSubscription_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn]),
	 CONSTRAINT [FK_ScheduleSubscription_User] FOREIGN KEY([UserFK]) REFERENCES [dbo].[User] ([ID]),
	 CONSTRAINT [FK_ScheduleSubscription_Doctor] FOREIGN KEY([DoctorFK]) REFERENCES [dbo].[Doctor] ([ID]),
	 CONSTRAINT [Valid_Dates] CHECK([RangeFrom] <= [RangeTo]) 
  ) 
  GO CREATE NONCLUSTERED INDEX [ScheduleSubscription_DeletedOn_Uid] 
	ON [dbo].[ScheduleSubscription] ([Uid],[DeletedOn])
  GO CREATE NONCLUSTERED INDEX [ScheduleSubscription_DeletedOn_Suid] 
	ON [dbo].[ScheduleSubscription] ([Suid],[DeletedOn])
