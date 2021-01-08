--Task 1

SELECT [FirstName] ,[LastName] FROM Employees
WHERE FirstName LIKE 'SA%'

-- Task 2
SELECT [FirstName] ,[LastName] FROM Employees
WHERE LastName LIKE '%ei%'

--Task 3
SELECT [FirstName] FROM Employees
WHERE DepartmentID =3 OR DepartmentID=10 AND DATEPART(YEAR,HireDate) > 1995 AND DATEPART(YEAR,HireDate)<=2005

--Task 4
SELECT[FirstName],LastName FROM Employees
WHERE JobTitle  NOT LIKE '%engineer%'

--Task 5
SELECT * FROM Towns
WHERE LEN([Name])=5 OR LEN([Name])=6 
ORDER BY [Name]


--Task 6
SELECT * FROM Towns
WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name]


--Task 7
SELECT * FROM Towns
WHERE [Name] LIKE '[^MKBE]%'
ORDER BY [Name];


-- Task 8
CREATE VIEW V_EmployeesHiredAfter2000 AS 
SELECT [FirstName],[LastName] FROM Employees
WHERE DATEPART(YEAR,HireDate)>2000


-- Task 9
SELECT[FirstName],LastName FROM Employees
WHERE LEN(LastName)=5

--Task 10

SELECT [EmployeeID]
      ,[FirstName]
      ,[LastName]
      ,[MiddleName]
      ,[JobTitle]
      ,[DepartmentID]
      ,[ManagerID]
      ,[HireDate]
      ,[Salary]
      ,[AddressID]
	  ,DENSE_RANK() OVER( PARTITION BY Salary ORDER BY EmployeeID) AS Rank
  FROM [SoftUni].[dbo].[Employees]
  WHERE Salary>10000 AND Salary<50000 
 
 

 -- TASK 11
 SELECT * FROM
(SELECT [EmployeeID]
      ,[FirstName]
      ,[LastName]
      ,[MiddleName]
      ,[JobTitle]
      ,[DepartmentID]
      ,[ManagerID]
      ,[HireDate]
      ,[Salary]
      ,[AddressID]
	  ,DENSE_RANK() OVER( PARTITION BY Salary ORDER BY EmployeeID) AS Rank
  FROM [SoftUni].[dbo].[Employees]
  WHERE Salary>10000 AND Salary<50000 ) AS b
	WHERE Rank=2
	ORDER BY Salary DESC 

--Task 12 
SELECT [CountryName] , IsoCode FROM Countries
WHERE LEN(CountryName)-LEN(REPLACE(CountryName,'A',''))=3
ORDER BY IsoCode
	
--Task 13


 --TASK 14
 SELECT TOP(50) [Name] ,FORMAT(Start,'yyyy-MM--dd') FROM Games
 WHERE DATEPART(YEAR,Start)>=2011 AND DATEPART(YEAR,Start)<=2012 
 ORDER BY Start ,Name


 --TASK 15
 SELECT [UserName],Email ,SUBSTRING(Email,(CHARINDEX('@',Email)+1),LEN(Email))AS [Email Provider] 
 FROM Users
 ORDER BY [Email Provider] ,Username

 --TASK 16
 SELECT Username ,IpAddress FROM Users
 WHERE IpAddress LIKE '___.1_%._%.___'
 ORDER BY Username

 --TASK 17
 
  SELECT [Name] AS p FROM Games 
  

	
 