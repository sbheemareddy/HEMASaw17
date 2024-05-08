USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetSystemData]    Script Date: 08-05-2024 05:48:08 PM ******/
DROP PROCEDURE [dbo].[spGetSystemData]
GO

/****** Object:  StoredProcedure [dbo].[spGetSystemData]    Script Date: 08-05-2024 05:48:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--select top 10 * from slicedata order by 1 desc      
      
----10253      
      
  -- spGetSystemData 1794498, '0000198336' ,'190953'   ,1,1    
CREATE PROCEDURE [dbo].[spGetSystemData]                     
 @workOrder int ,                  
 @slice_batch varchar(10),                  
 @block_batch varchar(10),            
 @sliceNum int ,        
 @IsScanned bit        
AS                    
BEGIN                    
 -- SET NOCOUNT ON added to prevent extra result sets from                    
 SET NOCOUNT ON;             
           
 declare @hasPrevious bit = 0           
 declare @hasLast bit =0          
 declare @maxSliceNum int = 1          
          
 SELECT @maxSliceNum = Max(sd.SliceNum)          
 FROM [HEMASaws].[dbo].WorkOrderHeader woh                    
 INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material                    
 INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder               
 WHERE woh.WorkOrder = @workOrder                    
 and (woh.Slice_Batch = @slice_batch  OR  woh.Slice_Batch is null)              
 and (woh.Block_Batch = @block_batch  OR  woh.Block_Batch is null)     
   
 IF @maxSliceNum IS NULL  
BEGIN  
    SET @maxSliceNum = 1;  
END  
          
If exists (SELECT  1 FROM [HEMASaws].[dbo].WorkOrderHeader woh                    
 INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material                    
 INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder               
 WHERE woh.WorkOrder = @workOrder                    
 and (woh.Slice_Batch = @slice_batch  OR  woh.Slice_Batch is null)              
 and (woh.Block_Batch = @block_batch  OR  woh.Block_Batch is null)            
 and (sd.SliceNum < @sliceNum ))          
BEGIN          
 SET @hasPrevious =1          
END          
          
If exists (SELECT  1 FROM [HEMASaws].[dbo].WorkOrderHeader woh                    
 INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material                    
 INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder               
 WHERE woh.WorkOrder = @workOrder                    
 and (woh.Slice_Batch = @slice_batch  OR  woh.Slice_Batch is null)              
 and (woh.Block_Batch = @block_batch  OR  woh.Block_Batch is null)            
 and (sd.SliceNum > @sliceNum ))          
BEGIN          
 SET @hasLast =1          
END          
                  
 IF (@slice_batch ='')                  
 set  @slice_batch = null                  
                  
 IF (@block_batch ='')                  
 set  @block_batch = null          
         
 if ( @IsScanned = 1)        
 Begin        
  SELECT  top 1                
   woh.Material,                
   WOH.Slice_Batch,                  
   im.Density,                   
   im.DensityTol,                   
   im.VisualPartID ,                   
   im.Description,                  
   @sliceNum as 'SliceNum',                  
   im.Thickness as Thickness,            
   0.0 as 'Length',            
   0.0 as 'Width',            
   0.0 as 'Weight',      
   '' as  Comments,      
   im.TargetCellCount,             
   im.MinCellCount,             
   im.MaxCellCount ,          
   @hasPrevious as hasPrevious,          
   @hasLast as hasLast,          
   @maxSliceNum as 'LastSliceNum',
   '' as CellCount
  FROM [HEMASaws].[dbo].WorkOrderHeader woh                    
  INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material                    
  WHERE woh.WorkOrder = @workOrder                    
  and (woh.Slice_Batch = @slice_batch  OR  woh.Slice_Batch is null)              
  and (woh.Block_Batch = @block_batch  OR  woh.Block_Batch is null)            
 END        
        
 ELSE        
 BEGIN        
  SELECT   top 1               
   woh.Material,                
   WOH.Slice_Batch,                  
   im.Density,                   
   im.DensityTol,                   
   im.VisualPartID ,                   
   im.Description,                  
   @sliceNum as 'SliceNum',                  
   sd.Thickness as Thickness,            
   sd.Length as 'Length',            
   sd.Width as 'Width',            
   sd.Weight as 'Weight',      
   sd.Comments,      
   im.TargetCellCount,             
   im.MinCellCount,             
   im.MaxCellCount ,          
   @hasPrevious as hasPrevious,          
   @hasLast as hasLast,          
   @maxSliceNum as 'LastSliceNum',
   sd.CellCount
  FROM [HEMASaws].[dbo].WorkOrderHeader woh                    
  INNER JOIN [HEMASaws].[dbo].[ItemMaster] im on im.Material= woh.Material                    
  INNER JOIN [HEMASaws].[dbo].[SliceData] sd on sd.WorkOrder =woh.WorkOrder               
  WHERE woh.WorkOrder = @workOrder                    
  and (woh.Slice_Batch = @slice_batch  OR  woh.Slice_Batch is null)              
  and (woh.Block_Batch = @block_batch  OR  woh.Block_Batch is null)            
  and (sd.SliceNum =@sliceNum)         
 END        
                    
            
                
END 
GO


