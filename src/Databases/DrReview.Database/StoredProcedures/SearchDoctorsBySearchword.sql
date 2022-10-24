CREATE PROCEDURE [dbo].[SearchDoctorsBySearchword]
	@searchword nvarchar(400) = '',
	@currentUserUid UNIQUEIDENTIFIER,
	@filterSchedules BIT = 0
AS
BEGIN

declare @formatedsearchword nvarchar(420)='"'+@searchword+'*"'
IF(@searchword <> '' AND @filterSchedules = 0)
BEGIN
	SELECT * FROM [dbo].[Doctor] as D
	WHERE CONTAINS(D.FullTextSearch, @formatedsearchword)
	AND D.DeletedOn IS NULL
END
ELSE IF (@searchword <> '' AND @filterSchedules = 1 AND @currentUserUid IS NOT NULL)
BEGIN
	SELECT D.* FROM [dbo].[Doctor] as D
	INNER JOIN [dbo].[User] AS U ON U.Uid = @currentUserUid
	WHERE CONTAINS(D.FullTextSearch, @formatedsearchword)
	AND NOT EXISTS(SELECT * FROM [dbo].[ScheduleSubscription] AS S WHERE S.DeletedOn IS NULL AND S.DoctorFK = D.ID AND S.UserFK = U.ID)
	AND D.DeletedOn IS NULL AND U.DeletedOn IS NULL
END
END
GO