DECLARE @s varchar(100)
--SET @s = 'GRP05TCY'
--SET @s = '%GRP%'
SET @s = 'GRP'
--SET @s = '%127897%'
--SET @s = '%Acme%'
--SET @s = '%Emca%'

SELECT DISTINCT
--SELECT
	TOP(250)
	--p.PackageIdentifier,sa.Manufacturer, pd.LicenceType, sa.Version, Gsr.GsrRef
	p.PackageIdentifier,sa.Manufacturer, pd.LicenceType, sa.Version
	
FROM
	Packages p	
		INNER JOIN
		PackageDetails pd ON p.Id = pd.PackageId
		INNER JOIN
		SoftwareAutomation sa ON sa.PackageId = p.Id
		LEFT OUTER JOIN
		Gsr ON sa.Id = Gsr.SotwareAutomationId

WHERE
	(p.PackageIdentifier LIKE @s)
	OR
	(Gsr.GsrRef LIKE @s)
	OR
	(sa.Manufacturer LIKE @s)

GROUP BY 
	sa.Manufacturer, p.PackageIdentifier, pd.LicenceType, sa.Version, Gsr.GsrRef

ORDER BY 
	sa.Manufacturer ASC, p.PackageIdentifier ASC

---------------------------------------------------

