--TASK 1
	CREATE PROC usp_GetEmployeesSalaryAbove35000
	AS
		BEGIN
		SELECT FirstName,LastName FROM Employees as e 
		WHERE Salary>35000

		END



-- TASK 2
CREATE PROC usp_GetEmployeesSalaryAboveNumber
(@number DECIMAL(18,4))
AS
	BEGIN
	SELECT FirstName,LastName FROM Employees 
	WHERE Salary>=@number

	END

--TASK 3
CREATE PROC usp_GetTownsStartingWith 
(@string NVARCHAR(50))
AS 
	BEGIN
	SELECT [Name] FROM Towns
	WHERE Name LIKE(@string+'%')
	END


--TASK 4
CREATE PROC usp_GetEmployeesFromTown
(@townName NVARCHAR(50))
AS
BEGIN
	SELECT FirstName,LastName FROM Employees as e
	JOIN Addresses AS  a
	ON e.AddressID=a.AddressID
	JOIN Towns AS t
	ON a.TownID=t.TownID
	WHERE t.Name =@townName

END

--TASK 5
CREATE OR ALTER FUNCTION udf_GetSalaryLevel
(@salary DECIMAL(18,4))
RETURNS NVARCHAR(30)
AS
BEGIN
DECLARE @result NVARCHAR(30)
IF @salary< 30000  SET @result= 'Low'
ELSE IF @salary<=50000 SET @result= 'Average'
ELSE   SET @result ='High'
RETURN @result

END




--TASK 6
CREATE OR ALTER PROC usp_EmployeesBySalaryLevel
(@salaryLevel NVARCHAR(10))
AS 
BEGIN
	SELECT FirstName,LastName FROM Employees
	WHERE dbo.udf_GetSalaryLevel(Salary)= @salaryLevel


END

--TASK 7 
CREATE FUNCTION udf_IsWordComprised 
(@setOfLetters NVARCHAR (20),@word NVARCHAR(50) )
RETURNS BIT
AS
BEGIN
 DECLARE @count AS INT =1;
 WHILE(@count<LEN(@word))
	BEGIN
	DECLARE @currentLetter AS VARCHAR(1)
	SET @currentLetter =  SUBSTRING(@word,@count,1)
	IF(CHARINDEX(@currentLetter,@setOfLetters)=0)
		BEGIN
		RETURN 0
		 END
	SET @count+=1;
	END
	RETURN 1
	
END 

SELECT 
[dbo].[udf_IsWordComprised] ('abcdf','ggg')




--TASK 8 !!!!!!!!!!!
CREATE OR ALTER PROC usp_EmployeesBySalaryLevel
(@departmentId INT)
AS
	ALTER TABLE Departments 
	ALTER COLUMN ManagerID  INT NULL 

	DELETE FROM Employees WHERE
	DepartmentID=@departmentId

	DELETE FROM Departments 
	WHERE DepartmentID=@departmentId

	SELECT COUNT(*) FROM Employees
	WHERE DepartmentID=@departmentId


GO

--TASK 9
CREATE PROC usp_GetHoldersFullName
AS

SELECT FirstName+ ' ' +LastName AS [Full Name]  FROM AccountHolders

GO



--TASK 10
CREATE PROC  usp_GetHoldersWithBalanceHigherThan
(@number INT)
AS
SELECT h.FirstName,h.LastName FROM Accounts AS a
JOIN AccountHolders AS h
ON a.AccountHolderId=h.Id
WHERE a.Balance>@number
ORDER BY FirstName,LastName

GO

--TASK 11
CREATE OR ALTER FUNCTION ufn_CalculateFutureValue
(@sum DECIMAL(10,5),@yeareRate FLOAT ,@numberOfYears INT)
RETURNS DECIMAL(10,4)
AS
BEGIN

WHILE (@numberOfYears>0)
BEGIN
SET @sum = @sum + (@yeareRate * @sum);
SET @numberOfYears=@numberOfYears-1;

END
RETURN @sum
END

--TASK 12
CREATE OR ALTER PROC usp_CalculateFutureValueForAccount
(@accountId INT, @interestRate FLOAT)
AS
SELECT 
		h.FirstName,
		h.LastName,
		a.Balance,
		dbo.ufn_CalculateFutureValue(CONVERT(DECIMAL(20,5),a.Balance),@interestRate,5) AS [Balance in 5 years]
	
	FROM Accounts as a
	JOIN AccountHolders as h
	ON a.AccountHolderId=h.Id
	WHERE  a.Id=@accountId
GO


--TASK 13

CREATE OR ALTER FUNCTION ufn_CashInUserGames
(@gameName NVARCHAR(50))
RETURNS @result TABLE(
SumCash DECIMAL(20,5)
)
AS
BEGIN

WITH MyCte AS(
SELECT Name ,ROW_NUMBER () OVER ( ORDER BY Cash DESC  ) AS RowOrder,Cash
	FROM 
	(SELECT g.Name,u.Cash FROM 
	UsersGames AS u
	JOIN Games AS g
	ON u.GameId=g.Id
	WHERE g.Name=@gameName)   AS [Tepm]
	)


INSERT INTO @result (SumCash) SELECT SUM(Cash) FROM MyCte
	WHERE RowOrder%2!=0
RETURN 

END 




