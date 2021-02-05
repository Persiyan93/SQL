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
WHERE [Name] LIKE '[^RBD]%'
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
		,[Salary]
      ,DENSE_RANK() OVER( PARTITION BY Salary ORDER BY  EmployeeID   ) AS Rank
  FROM Employees
  WHERE Salary>=10000 AND Salary<=50000 
  ORDER BY Salary DESC 
 
 

 -- TASK 11
 SELECT * FROM
(SELECT [EmployeeID]
      ,[FirstName]
      ,[LastName]
      ,[Salary]
   ,DENSE_RANK() OVER( PARTITION BY Salary ORDER BY EmployeeID) AS Rank
  FROM Employees
  WHERE Salary>=10000 AND Salary<=50000 ) AS b
	WHERE Rank=2
	ORDER BY Salary DESC 

--Task 12 
SELECT [CountryName] AS [Country Name] , IsoCode AS [ISO Code ] FROM Countries
WHERE LEN(CountryName)-LEN(REPLACE(CountryName,'A',''))>=3
ORDER BY IsoCode
	
--Task 13
SELECT  p.PeakName ,r.RiverName,LOWER(CONCAT(LEFT(p.PeakName,LEN(p.PeakName)-1),r.RiverName)) AS MIX FROM  Peaks AS p
CROSS JOIN  Rivers AS r
WHERE SUBSTRING(p.PeakName,LEN(p.PeakNAme),1)=LEFT(r.RiverName,1)
ORDER BY MIX


 --TASK 14
 SELECT TOP(50) [Name] ,FORMAT(Start,'yyyy-MM-dd') AS Start FROM Games
 WHERE DATEPART(YEAR,Start)=2011 OR DATEPART(YEAR,Start)=2012 
 ORDER BY Start ,Name


 --TASK 15
 SELECT [UserName] ,SUBSTRING(Email,(CHARINDEX('@',Email)+1),LEN(Email))AS [Email Provider] 
 FROM Users
 ORDER BY [Email Provider] ,Username

 --TASK 16
 SELECT Username ,IpAddress FROM Users
 WHERE IpAddress LIKE '___.1_%._%.___'
 ORDER BY Username

 --TASK 17

SELECT Name AS Game,
	CASE 
		WHEN DATEPART(hour,Start)>=0 AND DATEPART(hour,Start)<12 THEN 'Morning'
		WHEN DATEPART(hour,Start)>=12 AND DATEPART(hour,Start)<18 THEN 'Afternoon'
		WHEN DATEPART(hour,Start)>=18 AND DATEPART(hour,Start)<24 THEN 'Evening'
	END AS [Part of the Day],
	CASE
		WHEN Duration <=3 THEN 'Extra Short'
		WHEN Duration >= 4   AND Duration<=6 THEN 'Short'
		WHEN Duration >6   THEN 'Long'
		ELSE 'Extra Long'
	END AS Duration

FROM Games 
ORDER BY Name,Duration,[Part of the Day]
  

		
--TASK 18
SELECT ProductName ,
		OrderDate,
		DATEADD(DAY,3,OrderDate) AS [Pay Due],
		DATEADD(MONTH,1,OrderDate) AS [Deliver Due]
FROM Orders
 