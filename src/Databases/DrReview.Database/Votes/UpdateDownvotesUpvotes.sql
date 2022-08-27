CREATE TRIGGER [UpdateDownvotesUpvotesOnUpdate]
	ON [dbo].[Vote]
	AFTER DELETE, INSERT, UPDATE
	AS
	BEGIN

	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM inserted)
	BEGIN
		UPDATE     [dbo].[Review]
		SET        Upvotes = (SELECT COUNT(*) FROM [dbo].[Vote] as v WHERE v.ReviewFK = i.ReviewFK AND v.Upvoted = 1 AND v.DeletedOn IS NULL)
		FROM       inserted i
		INNER JOIN [dbo].[Review] as R
		ON         R.ID = i.ReviewFK

		UPDATE     [dbo].[Review]
		SET        Downvotes = (SELECT COUNT(*) FROM [dbo].[Vote] as v WHERE v.ReviewFK = i.ReviewFK AND v.Upvoted = 0 AND v.DeletedOn IS NULL)
		FROM       inserted i
		INNER JOIN [dbo].[Review] as R
		ON         R.ID = i.ReviewFK
	END
	ELSE IF EXISTS (SELECT 1 FROM deleted)
	BEGIN
		UPDATE     [dbo].[Review]
		SET        Upvotes = (SELECT COUNT(*) FROM [dbo].[Vote] as v WHERE v.ReviewFK = d.ReviewFK AND v.Upvoted = 1 AND v.DeletedOn IS NULL)
		FROM       deleted d
		INNER JOIN [dbo].[Review] as R
		ON         R.ID = d.ReviewFK

		UPDATE     [dbo].[Review]
		SET        Downvotes = (SELECT COUNT(*) FROM [dbo].[Vote] as v WHERE v.ReviewFK = d.ReviewFK AND v.Upvoted = 0 AND v.DeletedOn IS NULL)
		FROM       deleted d
		INNER JOIN [dbo].[Review] as R
		ON         R.ID = d.ReviewFK
	END

	END
