USE [SoftwareApplications]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[BroadSearch]
		--@s = N'%GRP%'
		@s = 'GRP05TCY'
		--@s = '%GRP%'
		--@s = 'GRP'
		--@s = '%127897%'
		--@s = '127897'
		--@s = 'Acme'
		--@s = '%Acm%'
		--@s = 'Acm'
		--@s = '%Emca%'

GO
