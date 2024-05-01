USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetUserByID]    Script Date: 01-05-2024 07:58:52 PM ******/
DROP PROCEDURE [dbo].[spGetUserByID]
GO

/****** Object:  StoredProcedure [dbo].[spGetUserByID]    Script Date: 01-05-2024 07:58:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:      
-- Create date: 02/29/2024    
-- Description: upsert the Employee Data    
-- =============================================    
CREATE PROCEDURE [dbo].[spGetUserByID]    
    @EmployeeID varchar(10)    
AS    
BEGIN    
SELECT top 1 [EmployeeID]    
      ,[FirstName]    
      ,[LastName]    
      ,[Active]    
      ,[CreateDate]    
      ,[TermDate]    
      ,[employeeRole]
	  ,[Password]
	  ,[bFirstTimeLogin]
  FROM [dbo].[Employee]    
  where EmployeeID = @employeeID    
END   
GO


