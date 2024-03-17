-- =============================================
-- Author:		
-- Create date: 02/29/2024
-- Description:	upsert the Employee Data
-- =============================================
ALTER PROCEDURE UpsertEmployee
    @EmployeeID VARCHAR(10),
    @FirstName VARCHAR(30),
    @LastName VARCHAR(30),
    @Active VARCHAR(1),
    @CreateDate DATE = NULL,
    @TermDate DATE = NULL,
	@employeeRole int =1
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
    END
    ELSE
    BEGIN
        -- Insert a new employee
        INSERT INTO Employee (EmployeeID, FirstName, LastName, Active, CreateDate, TermDate, employeeRole)
        VALUES (@EmployeeID, @FirstName, @LastName, @Active, ISNULL(@CreateDate, GETDATE()), @TermDate, @employeeRole)
    END
END
