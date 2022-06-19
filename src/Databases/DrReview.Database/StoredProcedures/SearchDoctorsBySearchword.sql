CREATE PROCEDURE [dbo].[SearchDoctorsBySearchword]
	@searchword nvarchar(400) = '',
	@skip int = 0,
	@take int = 1000
AS
BEGIN
IF(@searchword <> '')
BEGIN
	SELECT * FROM [dbo].[Doctor] as D
	WHERE CONTAINS(D.FullTextSearch, @searchword)
	AND D.DeletedOn IS NULL
	ORDER BY D.ID DESC
	OFFSET @skip ROWS
	FETCH NEXT @take ROWS ONLY
END
END
