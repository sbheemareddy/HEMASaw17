CREATE PROCEDURE [dbo].[spAcceptSliceData] 
 @id int,
 @employeeID varchar(10) ,          
 @expanderNum varchar(10),    
 @length numeric(18,5) ,
 @width numeric(18,5),
 @weight numeric(18,5),
 @comments varchar(80),
 @densityPCF numeric(18,5),
 @densityPSF numeric(18,5)
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
 WHERE ID = @id
END

