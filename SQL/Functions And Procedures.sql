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
CREATE  FUNCTION udf_GetSalaryLevel
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
CREATE PROC usp_EmployeesBySalaryLevel
(@salaryLevel NVARCHAR(10))
AS 
BEGIN
	SELECT FirstName,LastName FROM Employees
	WHERE dbo.ufn_GetSalaryLevel(Salary)= @salaryLevel


END

--TASK 7 
CREATE FUNCTION dbo.ufn_IsWordComprised
(@setOfLetters NVARCHAR (MAX),@word NVARCHAR(MAX) )
RETURNS BIT
AS
BEGIN
	DECLARE @count AS INT = 1;
	 WHILE(@count<=LEN(@word))
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






--TASK 8 
CREATE PROCEDURE  dbo.usp_DeleteEmployeesFromDepartment
(@departmentId INT)
AS
	BEGIN
	ALTER TABLE Departments 
	ALTER COLUMN ManagerID  INT NULL 

	DELETE FROM Employees WHERE
	DepartmentID=@departmentId

	DELETE FROM Departments 
	WHERE DepartmentID=@departmentId

	SELECT COUNT(*) FROM Employees
	WHERE DepartmentID=@departmentId


END

--TASK 9
CREATE PROC usp_GetHoldersFullName
AS

SELECT FirstName+ ' ' +LastName AS [Full Name]  FROM AccountHolders

GO



--TASK 10
CREATE PROC  usp_GetHoldersWithBalanceHigherThan
(@number DECIMAL(25,5))
AS
BEGIN

	SELECT h.FirstName,h.LastName FROM Accounts AS a
	JOIN AccountHolders AS h
	ON a.AccountHolderId=h.Id
	GROUP BY a.AccountHolderId,h.FirstName,h.LastName
	HAVING CONVERT(DECIMAL(25,5),SUM(Balance))>@number
	ORDER BY FirstName,LastName
END



--TASK 11
CREATE  FUNCTION ufn_CalculateFutureValue
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

SELECT dbo.ufn_CalculateFutureValue(1000,0.1,5)

--TASK 12
CREATE PROC usp_CalculateFutureValueForAccount
(@accountId INT, @interestRate FLOAT)
AS
BEGIN
SELECT 
		h.Id,
		h.FirstName,
		h.LastName,
		a.Balance,
		dbo.ufn_CalculateFutureValue(CONVERT(DECIMAL(20,5),a.Balance),@interestRate,5) AS [Balance in 5 years]
	
	FROM Accounts as a
	JOIN AccountHolders as h
	ON a.AccountHolderId=h.Id
	WHERE  a.Id=@accountId
END


--TASK 13

CREATE  FUNCTION dbo.ufn_CashInUsersGames
(@gameName NVARCHAR(50))
RETURNS @result TABLE(
SumCash money
)
AS
BEGIN

WITH MyCte(Cash,Row) AS(
	SELECT ug.Cash, ROW_NUMBER() OVER (PARTITION  BY GameId ORDER BY Cash DESC ) AS Row FROM UsersGames AS ug
	JOIN Games AS g ON
	ug.GameId=g.Id
	WHERE Name LIKE (@gameName)
	)


INSERT INTO @result (SumCash) SELECT SUM(Cash) FROM MyCte
	WHERE Row%2!=0
RETURN 

END 

SELECT * FROM ufn_CashInUsersGames ('Shruikan')

SELECT * ,ROW_NUMBER() OVER (PARTITION  BY GameId ORDER BY Cash DESC )FROM UsersGames AS ug
JOIN Games AS g ON
ug.GameId=g.Id
WHERE Name LIKE('Love%')

