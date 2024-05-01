USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spGetEmployee]    Script Date: 01-05-2024 07:54:31 PM ******/
DROP PROCEDURE [dbo].[spGetEmployee]
GO

/****** Object:  StoredProcedure [dbo].[spGetEmployee]    Script Date: 01-05-2024 07:54:31 PM ******/
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


