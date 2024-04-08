USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetSliceLabel]    Script Date: 08-04-2024 07:12:50 AM ******/
DROP PROCEDURE [dbo].[spGetSliceLabel]
GO

/****** Object:  StoredProcedure [dbo].[spGetSliceLabel]    Script Date: 08-04-2024 07:12:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSliceLabel]     
 @workOrder int ,  
 @slice_batch varchar(10)=null,  
 @block_batch varchar(10) = null ,
 @sliceNum int
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 SET NOCOUNT ON;    
  
  IF (@slice_batch ='')  
  set  @slice_batch = null  
  
  IF (@block_batch ='')  
  set  @block_batch = null  
 select   
  woh.Material  
  ,woh.Slice_Batch  
  ,woh.Block_Batch  
  ,woh.WorkOrder  
  ,woh.Release_Date  
  ,CAST(woh.Scheduled_Qty  AS INT) AS Scheduled_Qty 
  ,woh.SalesOrder  
  ,woh.Mfg_date  
  ,woh.VisualPartID
  ,Im.[Description]  
  ,SD.DensityPCF  
  ,SD.DensityPSF  
  ,CAST(SD.MinThk AS DECIMAL(10,3)) AS MinThk
  ,CAST(SD.MaxThk AS DECIMAL(10,3)) AS MaxThk
  ,CAST(SD.AvgThk AS DECIMAL(10,3)) AS AvgThk
  ,    CAST(Sd.SawNum AS VARCHAR(10)) + ' - ' + CAST(SD.SliceNum AS VARCHAR(10)) AS SliceNum
  , (select Lastname + ' ' + FirstName from Employee where EmployeeID = sd.EmployeeID) as 'OperatorName'
  ,'F137-2 - 10/22' as DocNum
 from WorkOrderHeader WOH  
 Inner Join itemmaster IM on IM.Material = WOH.Material  
 Inner Join SliceData SD on SD.WorkOrder =WOH.WorkOrder  
 where woh.WorkOrder =@workOrder  
 and woh.Slice_batch =@slice_batch  
 and ( woh.Block_Batch = @block_batch)  
 and sd.SliceNum = @sliceNum
END   

--select distinct EmployeeID from SliceData
--select * from Employee

--0111
GO


