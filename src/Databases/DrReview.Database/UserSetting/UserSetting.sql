CREATE TABLE [dbo].[UserSetting]
  (
     [ID]           BIGINT                      NOT NULL IDENTITY PRIMARY KEY,
     [Uid]          UNIQUEIDENTIFIER            NOT NULL UNIQUE,
     [Suid]         NVARCHAR(200)               NOT NULL UNIQUE,
     [DeletedOn]    DATETIME2(7)                NULL,
     [ModifiedOn]   SMALLDATETIME               NOT NULL DEFAULT GETUTCDATE(),
     [UserFK]       BIGINT                      NOT NULL,
     [UseSystemDefaultTheme] BIT                NOT NULL DEFAULT 1,
     [DarkTheme]    BIT                         NOT NULL DEFAULT 1,
     [AutomaticTheme] BIT                       NOT NULL DEFAULT 0,
     [NotifyExpiringSubscriptions] BIT          NOT NULL DEFAULT 0,
     [NotifyCloseToExpiryingSubscriptions] BIT  NOT NULL DEFAULT 0,
     CONSTRAINT [UserSetting_Uid_DeletedOn] UNIQUE([Uid], [DeletedOn]),
     CONSTRAINT [UserSetting_Suid_DeletedOn] UNIQUE([Suid], [DeletedOn]),
     CONSTRAINT [FK_UserSetting_User] FOREIGN KEY([UserFK]) REFERENCES [dbo].[User] ([ID])
  ) 
 GO CREATE NONCLUSTERED INDEX [UserSetting_DeletedOn_Uid] 
    ON [dbo].[UserSetting] ([Uid],[DeletedOn])
 GO CREATE NONCLUSTERED INDEX [UserSetting_DeletedOn_Suid] 
    ON [dbo].[UserSetting] ([Suid],[DeletedOn])