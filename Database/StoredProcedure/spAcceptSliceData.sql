USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spAcceptSliceData]    Script Date: 08-05-2024 05:48:59 PM ******/
DROP PROCEDURE [dbo].[spAcceptSliceData]
GO

/****** Object:  StoredProcedure [dbo].[spAcceptSliceData]    Script Date: 08-05-2024 05:48:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
CREATE PROCEDURE [dbo].[spAcceptSliceData]           
 @id int,          
 @employeeID varchar(10) ,                    
 @expanderNum varchar(10),              
 @length numeric(18,5) ,          
 @width numeric(18,5),          
 @weight numeric(18,5),          
 @comments varchar(80),          
 @densityPCF numeric(18,5),          
 @densityPSF numeric(18,5),        
 @QrCodeDate date,        
 @CellCount int,      
 @workOrder varchar(30),      
 @block_batch varchar(10),         
 @sliceNum int,      
 @saw int,      
 @max numeric(18,5),       
 @min numeric(18,5),       
 @ave numeric(18,5),  
 @badge varchar(10) = ''    
AS                      
BEGIN       
    


if ( @id < 0)
Begin
if Exists (	select 1 from [dbo].[SliceData] where WorkOrder = @workOrder 
							and BlockNumber = @block_batch 
							and SliceNum = @sliceNum
							and SawNum = @saw
							and MinThk = @min
							and MaxThk = @max
							and AvgThk = @ave
							and Badge = @badge)
			Begin
				select @id = Id from [dbo].[SliceData] where WorkOrder = @workOrder 
											and BlockNumber = @block_batch 
											and SliceNum = @sliceNum
											and SawNum = @saw
											and MinThk = @min
											and MaxThk = @max
											and AvgThk = @ave
											and Badge = @badge
	
			END
END

Declare @sliceID int = @id   
      
If Exists (select 1 from [dbo].[SliceData] where id = @id)      
 BEGIN      
  UPDATE [dbo].[SliceData]          
     SET [EmployeeID] = @employeeID          
     ,[ExpanderNum] = @expanderNum          
     ,[Length] = @length          
     ,[Width] = @width          
     ,[Weight] = @weight          
     ,[Comments] = @comments          
     ,[DensityPCF] = @densityPCF          
     ,[DensityPSF] = @densityPSF          
     ,[QRCodeDate]=@QrCodeDate        
     ,[CellCount] = @CellCount        
   WHERE ID = @id        
 END      
 ELSE      
 BEGIN      
 Declare  @thickness numeric(18,5)      
 select @thickness= [Thickness] from [dbo].[ItemMaster] im      
 inner join WorkOrderHeader wo on wo.Material = im.[Material]      
 where wo.workOrder = @workOrder      
  INSERT INTO [dbo].[SliceData]      
       ([WorkOrder]      
       ,[BlockNumber]      
       ,[SliceNum]      
       ,[EmployeeID]      
       ,[SawNum]      
       ,[ExpanderNum]      
       ,[Length]      
       ,[Width]      
       ,[Weight]      
       ,[MinThk]      
       ,[MaxThk]      
       ,[AvgThk]      
       ,[Comments]      
       ,[DensityPCF]      
       ,[DensityPSF]      
       ,[CellCount]      
       ,[QRCodeDate]      
       ,[Thickness]  
    ,[badge]  
       )      
    VALUES      
       (@workOrder      
       ,@block_batch      
       ,@sliceNum      
       ,@employeeID      
       ,@saw      
       ,@expanderNum      
       ,@length      
       ,@width      
       ,@weight      
       ,@min      
       ,@max      
       ,@ave      
       ,@comments      
       ,@densityPCF      
       ,@densityPSF      
       ,@CellCount      
       ,@QrCodeDate      
       ,@thickness  
    , @badge)      
     select @sliceID = @@IDENTITY    
 END      
    
 return @sliceID    
END          
GO


