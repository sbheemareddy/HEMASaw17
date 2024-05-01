USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetSearchWO]    Script Date: 01-05-2024 07:55:51 PM ******/
DROP PROCEDURE [dbo].[spGetSearchWO]
GO

/****** Object:  StoredProcedure [dbo].[spGetSearchWO]    Script Date: 01-05-2024 07:55:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--    spGetSearchWO 1685996, '000173837A' ,''      
-- =============================================            
-- Author:              
-- Create date: 02/29/2024            
-- Description: gets the System Data for the work Order            
-- =============================================            
CREATE PROCEDURE [dbo].[spGetSearchWO]             
 @workOrder int ,          
 @slice_batch varchar(10),          
 @block_batch varchar(10) ,
 @sliceNum int 
AS            
BEGIN            
 -- SET NOCOUNT ON added to prevent extra result sets from            
 SET NOCOUNT ON;  
 
 -- Create a temporary table named #TempWorkOrderDetails
CREATE TABLE #TempWorkOrderDetails (
    WorkOrder NVARCHAR(255),
    Block_Batch NVARCHAR(255),
    Material NVARCHAR(255),
    Slice_Batch NVARCHAR(255),
    Density FLOAT,
    DensityTol FLOAT,
    VisualPartID NVARCHAR(255),
    Description NVARCHAR(255),
    SliceNum INT,
    Thickness FLOAT,
    ID INT
);
          
 IF (@slice_batch ='')          
 set  @slice_batch = null          
          
 IF (@block_batch ='')          
 set  @block_batch = null          
     
 IF ( @slice_batch is Null and @block_batch is NULL)    
 BEGIN   
 INSERT INTO #TempWorkOrderDetails
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
  sd.Thickness,  
  sd.ID  
  FROM [HEMASaws].[dbo].WorkOrderHeader woh            
  INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material            
  INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder           
  WHERE woh.WorkOrder = @workOrder       
END    
    
 IF ( @slice_batch is NOT Null and @block_batch is NULL)    
 BEGIN 
 INSERT INTO #TempWorkOrderDetails
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
  sd.Thickness,  
  sd.ID          
  FROM [HEMASaws].[dbo].WorkOrderHeader woh            
  INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material            
  INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder           
  WHERE woh.WorkOrder = @workOrder            
  and (woh.Slice_Batch = @slice_batch)      
END     
    
IF ( @slice_batch is NOT Null and @block_batch is NOT NULL)    
 BEGIN    
 INSERT INTO #TempWorkOrderDetails
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
  sd.Thickness,  
  sd.ID                
  FROM [HEMASaws].[dbo].WorkOrderHeader woh            
  INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material            
  INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder           
  WHERE woh.WorkOrder = @workOrder            
  and (woh.Slice_Batch = @slice_batch )      
  and (woh.Block_Batch = @block_batch )          
 END  
 

 if (@sliceNum > 0)
 BEGIN
	select * from #TempWorkOrderDetails where SliceNum =@sliceNum
 END
 ELSE
 BEGIN
	select * from #TempWorkOrderDetails
 END
 
 drop table #TempWorkOrderDetails
    
 END 
GO


