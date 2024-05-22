USE [HEMASaws]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[spAcceptSliceData]
		@id = -1,
		@employeeID = N'0096',
		@expanderNum = N'1',
		@length = 152,
		@width = 43,
		@weight = 6.3,
		@comments = NULL,
		@densityPCF = 3.4845311195669431,
		@densityPSF = 0.13880048959608324,
		@QrCodeDate = '21-05-2024 12:00:00 AM',
		@CellCount = 10,
		@workOrder = N'1594221',
		@block_batch = N'0000177801',
		@sliceNum = 2,
		@saw = '10A',
		@max = 0.381,
		@min = 0.378,
		@ave = 0.379,
		@badge = N'sgsg'

SELECT	'Return Value' = @return_value

GO
