    
-- =============================================    
-- Author:      
-- Create date: 02/29/2024    
-- Description: gets the System Data for the work Order    
-- =============================================    
ALTER PROCEDURE [dbo].[spGetSystemData]     
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
 WHERE sd.WorkOrder = @workOrder    
 and Slice_Batch = @slice_batch  
 and sd.BlockNumber = @block_batch  
 and woh.Slice_Batch = @slice_batch  
END    
