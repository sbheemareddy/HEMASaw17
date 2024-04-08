USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetEmployee]    Script Date: 08-04-2024 07:11:06 AM ******/
DROP PROCEDURE [dbo].[spGetEmployee]
GO

/****** Object:  StoredProcedure [dbo].[spGetEmployee]    Script Date: 08-04-2024 07:11:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetEmployee]         
 @employeeID int      
AS        
BEGIN 
SELECT [EmployeeID]
      ,[FirstName]
      ,[LastName]
      ,[employeeRole]
  FROM [dbo].[Employee]
  where [EmployeeID] = @employeeID
  and Active = 'Y'

END


GO


