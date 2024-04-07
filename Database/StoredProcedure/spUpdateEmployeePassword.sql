-- =============================================    
-- Author:      
-- Create date: 02/29/2024    
-- Description: upsert the Employee Data    
-- =============================================    
CREATE PROCEDURE spUpdateEmployeePassword   
    @EmployeeID varchar(10)  ,
	@Password Varchar(250)
AS    
BEGIN    
Update [dbo].[Employee]    
 SET [Password] = @Password ,
     [bFirstTimeLogin] = 0 
  where EmployeeID = @employeeID    
END    


