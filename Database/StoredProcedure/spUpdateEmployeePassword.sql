USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateEmployeePassword]    Script Date: 08-04-2024 07:14:52 AM ******/
DROP PROCEDURE [dbo].[spUpdateEmployeePassword]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateEmployeePassword]    Script Date: 08-04-2024 07:14:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================    
-- Author:      
-- Create date: 02/29/2024    
-- Description: upsert the Employee Data    
-- =============================================    
CREATE PROCEDURE [dbo].[spUpdateEmployeePassword]   
    @EmployeeID varchar(10)  ,
	@Password Varchar(250)
AS    
BEGIN    
Update [dbo].[Employee]    
 SET [Password] = @Password ,
     [bFirstTimeLogin] = 0 
  where EmployeeID = @employeeID    
END    



GO


