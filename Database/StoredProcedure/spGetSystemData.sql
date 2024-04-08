USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetSystemData]    Script Date: 08-04-2024 07:13:34 AM ******/
DROP PROCEDURE [dbo].[spGetSystemData]
GO

/****** Object:  StoredProcedure [dbo].[spGetSystemData]    Script Date: 08-04-2024 07:13:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetSystemData]         
 @workOrder int ,      
 @slice_batch varchar(10),      
 @block_batch varchar(10),
 @sliceNum int
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
 sd.Thickness,
 sd.[Length],
 sd.Width,
 sd.[Weight],
 im.TargetCellCount,	
 im.MinCellCount,	
 im.MaxCellCount
 FROM [HEMASaws].[dbo].WorkOrderHeader woh        
 INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material        
 INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder   
 WHERE woh.WorkOrder = @workOrder        
 and (woh.Slice_Batch = @slice_batch  OR  woh.Slice_Batch is null)  
 and (woh.Block_Batch = @block_batch  OR  woh.Block_Batch is null)
 and (sd.SliceNum =@sliceNum)
    
END 
GO


