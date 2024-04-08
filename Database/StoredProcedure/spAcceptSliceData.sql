USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spAcceptSliceData]    Script Date: 08-04-2024 07:10:40 AM ******/
DROP PROCEDURE [dbo].[spAcceptSliceData]
GO

/****** Object:  StoredProcedure [dbo].[spAcceptSliceData]    Script Date: 08-04-2024 07:10:40 AM ******/
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
 @CellCount int
AS              
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
  

GO


