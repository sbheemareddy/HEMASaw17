 --    spGetSearchWO 1685996,	'000173837A' ,''
-- =============================================      
-- Author:        
-- Create date: 02/29/2024      
-- Description: gets the System Data for the work Order      
-- =============================================      
ALTER PROCEDURE [dbo].[spGetSearchWO]       
 @workOrder int ,    
 @slice_batch varchar(10),    
 @block_batch varchar(10)    
AS      
BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 SET NOCOUNT ON;      
    
 IF (@slice_batch ='')    
 set  @slice_batch = null    
    
 IF (@block_batch ='')    
 set  @block_batch = null    
      
 SELECT  
 woh.WorkOrder,
 woh.Block_Batch, 
 woh.Material,  
 WOH.Slice_Batch,    
 im.Density,     
 im.DensityTol,     
 im.VisualPartID ,     
 im.Description,    
 sd.SliceNum,    
 sd.Thickness    
 FROM [HEMASaws].[dbo].WorkOrderHeader woh      
 INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material      
 INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder     
 WHERE woh.WorkOrder = @workOrder      
 and (woh.Slice_Batch = @slice_batch  OR  woh.Slice_Batch is null)
 and (woh.Block_Batch = @block_batch  OR  woh.Block_Batch is null)    
  
END      