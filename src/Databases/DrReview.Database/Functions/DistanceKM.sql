CREATE FUNCTION dbo.DistanceKM(
@lat1 decimal(19,16),
@lon1 decimal(19,16),
@lat2 decimal(19,16),
@lon2 decimal(19,16))
RETURNS FLOAT 
AS
BEGIN

    RETURN ACOS(
        (SELECT MIN(x) FROM (VALUES (SIN(PI()*@lat1/180.0)*SIN(PI()*@lat2/180.0)+COS(PI()*@lat1/180.0)*COS(PI()*@lat2/180.0)*COS(PI()*@lon2/180.0-PI()*@lon1/180.0))
        , (1)) AS value(x))) * 6371
END
