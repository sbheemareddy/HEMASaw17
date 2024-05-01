USE [HEMASaws]
GO

/****** Object:  StoredProcedure [dbo].[UpsertEmployee]    Script Date: 01-05-2024 08:00:41 PM ******/
DROP PROCEDURE [dbo].[UpsertEmployee]
GO

/****** Object:  StoredProcedure [dbo].[UpsertEmployee]    Script Date: 01-05-2024 08:00:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		
-- Create date: 02/29/2024
-- Description:	upsert the Employee Data
-- =============================================
CREATE PROCEDURE [dbo].[UpsertEmployee]
    @EmployeeID VARCHAR(10),
    @FirstName VARCHAR(30),
    @LastName VARCHAR(30),
    @Active VARCHAR(1),
    @CreateDate DATE = NULL,
    @TermDate DATE = NULL,
	@employeeRole int =1,
	@Password Varchar(250),
	@bFirstTimeLogin bit = 1
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @EmployeeID)
    BEGIN
        -- Update the existing employee
        UPDATE Employee
        SET FirstName = @FirstName,
            LastName = @LastName,
            Active = @Active,
            CreateDate = ISNULL(@CreateDate, CreateDate),
            TermDate = @TermDate,
			employeeRole = @employeeRole
        WHERE EmployeeID = @EmployeeID

		if (@bFirstTimeLogin = 1)
		begin
			Update [dbo].[Employee]    
			 SET [Password] = @Password ,
				 [bFirstTimeLogin] = 1 
			  where EmployeeID = @employeeID    
		end
    END
    ELSE
    BEGIN
        -- Insert a new employee
        INSERT INTO Employee (EmployeeID, FirstName, LastName, Active, CreateDate, TermDate, employeeRole,[Password], bFirstTimeLogin)
        VALUES (@EmployeeID, @FirstName, @LastName, @Active, ISNULL(@CreateDate, GETDATE()), @TermDate, @employeeRole, @Password,1 )
    END
END
GO


