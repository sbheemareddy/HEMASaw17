Alter PROCEDURE [dbo].[spGetEmployee]         
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


