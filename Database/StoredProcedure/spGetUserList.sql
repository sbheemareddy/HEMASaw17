--============================================    
-- Author:    Srinivas  
-- Create date: 03/02/2024    
-- Description: gets the employee Data for the work Order    
-- =============================================    
ALTER PROCEDURE spGetUserList    
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
      ,CASE  
  WHEN employeeRole = 1 THEN 'Operator'  
  else 'Supervisor'  
  END AS 'employeeRole'  
  FROM [dbo].[Employee]  
 where Active = @active  
END    
    
    