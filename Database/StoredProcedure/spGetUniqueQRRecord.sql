USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetUniqueQRRecord]    Script Date: 08-05-2024 05:47:43 PM ******/
DROP PROCEDURE [dbo].[spGetUniqueQRRecord]
GO

/****** Object:  StoredProcedure [dbo].[spGetUniqueQRRecord]    Script Date: 08-05-2024 05:47:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
      
--spGetUniqueQRRecord 1685996,'123',1,10,0.382,0.3842,0.3822      
CREATE PROCEDURE [dbo].[spGetUniqueQRRecord]                   
 @workOrder Varchar(30) ,                
 @block_batch varchar(15),          
 @sliceNum int ,      
 @saw VARCHAR(10),      
 @max float,      
 @min float,      
@ave float      
AS                  
BEGIN                  
 -- SET NOCOUNT ON added to prevent extra result sets from                  
 SET NOCOUNT ON;                  
            
                  
 SELECT         
 woh.Workorder,      
 woh.Material,              
 WOH.Slice_Batch,          
 WOH.Block_Batch,       
 im.Density,                 
 im.DensityTol,                 
 im.VisualPartID ,                 
 im.Description,                
 @sliceNum as SliceNum,                
 im.Thickness,        
 @saw as 'SawNum',        
 @min as 'MinThk',        
 @max as 'MaxThk',        
 @ave as 'AvgThk',      
 -1 as 'ID'  
 FROM [HEMASaws].[dbo].WorkOrderHeader woh                  
 INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material                  
 --INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder                 
 WHERE woh.WorkOrder = @workOrder                  
 and (woh.Block_Batch = @block_batch)          
 --and (sd.SliceNum =@sliceNum)          
 --and MinThk = @min      
--and MaxThk =  @max      
--and AvgThk =@ave      
             
END     
    
GO


