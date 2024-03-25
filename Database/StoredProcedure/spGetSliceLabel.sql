USE [HEMASaws]
GO
ALTER PROCEDURE [dbo].[spGetSliceLabel]   
	@workOrder int ,
	@slice_batch	varchar(10)=null,
	@block_batch varchar(10) = null
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 SET NOCOUNT ON;  

	 IF (@slice_batch ='')
		set  @slice_batch = null

	 IF (@block_batch ='')
		set  @block_batch = null
	select 
		Top 1
		woh.Material
		,woh.Slice_Batch
		,woh.Block_Batch
		,woh.WorkOrder
		,woh.Release_Date
		,woh.Scheduled_Qty
		,woh.SalesOrder
		,woh.Mfg_date
		,Im.[Description]
		,SD.DensityPCF
		,SD.DensityPSF
		,SD.MinThk
		,SD.MaxThk
		,SD.AvgThk
		,SD.SliceNum
	from WorkOrderHeader WOH
	Inner Join itemmaster IM on IM.Material = WOH.Material
	Inner Join SliceData SD on SD.WorkOrder =WOH.WorkOrder
	where woh.WorkOrder =@workOrder
	and woh.Slice_batch =@slice_batch
	and ( woh.Block_Batch = @block_batch OR woh.Block_Batch IS NULL)
END 
GO


