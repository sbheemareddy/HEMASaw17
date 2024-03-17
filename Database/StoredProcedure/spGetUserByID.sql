  
  
-- =============================================  
-- Author:    
-- Create date: 02/29/2024  
-- Description: upsert the Employee Data  
-- =============================================  
ALTER PROCEDURE spGetUserByID  
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
  FROM [dbo].[Employee]  
  where EmployeeID = @employeeID  
END  
  
