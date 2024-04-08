USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetUserList]    Script Date: 08-04-2024 07:14:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--============================================      
-- Author:    Srinivas    
-- Create date: 03/02/2024      
-- Description: gets the employee Data for the work Order      
-- =============================================      
CREATE PROCEDURE [dbo].[spGetUserList]      
 @active varchar(1)= 'Y'    
AS      
BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 SET NOCOUNT ON;      
    
SELECT [EmployeeID]    
      ,[FirstName]    
      ,[LastName]    
      ,CASE    
  WHEN Active = 'Y' THEN 1    
  else 0    
  END AS 'Active'    
      ,[CreateDate]    
      ,[TermDate] 
	  ,[employeeRole]
      ,CASE    
  WHEN employeeRole = 1 THEN 'Operator'    
  else 'Supervisor'    
  END AS 'employeeRoleDesc'    
  FROM [dbo].[Employee]    
 where Active = @active    
END      
      
GO


