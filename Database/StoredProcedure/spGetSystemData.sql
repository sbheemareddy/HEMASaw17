USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetSystemData]    Script Date: 02-03-2024 09:50:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		
-- Create date: 02/29/2024
-- Description:	gets the System Data for the work Order
-- =============================================
ALTER PROCEDURE [dbo].[spGetSystemData] 
	@workOrder int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

	SELECT   
	CASE
		WHEN woh.[Status] = 'A' THEN 'A - Open'
		WHEN woh.[Status] = 'Y' THEN 'Y - Closed'
	END AS 'Status',	
	woh.Slice_Batch , im.[Description]
	FROM [HEMASaws].[dbo].WorkOrderHeader woh
	INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material
	WHERE WorkOrder = @workOrder
END

