--TASK 1

SELECT TOP(5) EmployeeID,JobTitle,a.AddressID,a.AddressText FROM Employees AS e
JOIN Addresses AS a ON
e.AddressID=a.AddressID
ORDER BY AddressID
--TASK 2
SELECT TOP(50) FirstName,LastName ,t.Name,a.AddressText FROM Employees AS e
	JOIN  Addresses AS a ON
		e.AddressID=a.AddressID
	JOIN  Towns AS t ON
	a.TownID=t.TownID
ORDER By FirstName ,LastName


--TASK 3
SELECT e.EmployeeID,e.FirstName,e.LastName,'Sales' AS [DepartmentName] FROM Employees AS e
WHERE e.DepartmentID IN
(SELECT DepartmentID FROM Departments AS d
  WHERE d.Name='Sales' )
 ORDER BY EmployeeID

 --TASK 4
 SELECT TOP (5) EmployeeID,FirstName,Salary ,d.Name FROM Employees AS e
 JOIN  Departments AS d ON
 e.DepartmentID=d.DepartmentID
 WHERE e.Salary >15000
 ORDER BY d.DepartmentID

 --TASK 5
 SELECT TOP(3) e.EmployeeID,e.FirstName FROM Employees AS e
 LEFT JOIN  EmployeesProjects AS p ON
 e.EmployeeID = p.EmployeeID
 WHERE p.ProjectID is NULL
 ORDER BY e.EmployeeID
 
 --TASK 6
  SELECT  e.FirstName,e.LastName ,e.HireDate,d.Name FROM Employees AS e
  JOIN  Departments AS d ON
 e.DepartmentID = d.DepartmentID
 WHERE (d.Name ='Sales' OR d.Name='Finance') AND e.HireDate>'1999-01-01'
 ORDER BY e.HireDate

 --TASK 7
  SELECT TOP(5) e.EmployeeID , e.FirstName,p.Name FROM Employees AS e
	JOIN  EmployeesProjects AS ep ON
		e.EmployeeID = ep.EmployeeID
	JOIN Projects AS p ON
		ep.ProjectID=p.ProjectID
	WHERE  p.StartDate >CAST('2002-08-13' AS smalldatetime) AND p.EndDate is NULL 
	ORDER BY e.EmployeeID

--TASK 8   

 SELECT  e.EmployeeID , e.FirstName ,p.StartDate,
  CASE 
	WHEN DATEPART(YEAR,p.StartDate)>=2005 THEN NULL
	ELSE p.Name
	END AS [ProjectName]
FROM Employees AS e
	JOIN  EmployeesProjects AS ep ON
		e.EmployeeID = ep.EmployeeID
	JOIN Projects AS p ON
		ep.ProjectID=p.ProjectID
	WHERE e.EmployeeID=24
	ORDER BY e.EmployeeID

--TASK 9
SELECT  e.EmployeeID,e.FirstName,e.ManagerID ,m.FirstName  FROM Employees AS e 
	JOIN Employees AS m ON
		e.ManagerID=m.EmployeeID
	WHERE e.ManagerID=3 OR e.ManagerID=7
	ORDER BY e.EmployeeID


--TASK 10
SELECT TOP (50)  e.EmployeeID,e.FirstName+' '+e.LastName AS EmploeyeeName  ,
				m.FirstName+' '+m.LastName AS ManagerName ,
				d.Name AS DepartmentName FROM Employees AS e
	JOIN Employees AS m
		ON e.ManagerID=m.EmployeeID
	JOIN Departments AS d
		ON e.DepartmentID=d.DepartmentID
	ORDER BY EmployeeID

--TASK 11
SELECT MIN(a.AverageSalary) AS MinAverageSalary FROM
	(SELECT AVG(e.Salary) AS AverageSalary FROM Employees AS e
	GROUP BY e.DepartmentID) AS a

--TASK 12

SELECT c.CountryCode AS CountryCode ,m.MountainRange AS  MountainRange,PeakName,
p.Elevation AS Elevation   FROM Peaks as p
	JOIN Mountains AS m ON 
		p.MountainId=m.Id
	JOIN MountainsCountries AS c ON
		m.Id=c.MountainId
	WHERE p.Elevation>2835 AND c.CountryCode='BG'
	ORDER BY p.Elevation DESC

-- TASK 13
SELECT c.CountryCode ,COUNT(m.MountainRange) AS MountainRanges FROM Mountains AS m
JOIN MountainsCountries AS c ON
m.Id= c.MountainId
WHERE c.CountryCode='BG' OR c.CountryCode='RU' OR  c.CountryCode='US'
GROUP BY (c.CountryCode)
ORDER BY MountainRanges DESC

--TASK 14

SELECT TOP(5) c.CountryName,r.RiverName   FROM Countries AS c
	 LEFT OUTER JOIN CountriesRivers AS cr ON
		c.CountryCode=cr.CountryCode
	LEFT OUTER JOIN Rivers AS r ON
		r.Id=cr.RiverId
		WHERE c.ContinentCode='AF'
	ORDER BY c.CountryName 




--TASK 15

SELECT  ContinentCode,CurrencyCode,CurrencyUsage FROM 
(SELECT ContinentCode ,CurrencyCode ,COUNT(CurrencyCode) AS CurrencyUsage,
	DENSE_RANK() OVER (PARTITION BY ContinentCode ORDER BY COUNT(CurrencyCode)DESC) AS Rank

FROM Countries
GROUP BY ContinentCode,CurrencyCode) AS temp
WHERE Rank=1 AND CurrencyUsage>1

--TASK 16
SELECT COUNT(*) FROM  Countries AS c
	LEFT OUTER JOIN  MountainsCountries AS mc ON
	c.CountryCode=mc.CountryCode
	WHERE mc.MountainId IS NULL
--TASK 17

SELECT TOP(5) c.CountryName, MAX(p.Elevation) AS HighestPeakEvaluation,MAX(r.Length)AS LongestRiverLength FROM CountriesRivers AS cr
JOIN Rivers AS r ON
cr.RiverId=r.Id
RIGHT JOIN Countries AS c ON
cr.CountryCode= c.CountryCode
 LEFT JOIN MountainsCountries AS mc ON
c.CountryCode=mc.CountryCode
 JOIN Mountains AS m ON
mc.MountainId=m.Id
 JOIN Peaks AS p ON
m.Id=p.MountainId
GROUP BY(c.CountryName)
ORDER BY HighestPeakEvaluation DESC ,LongestRiverLength DESC, c.CountryName

--TASK 18
SELECT TOP (5)  CountryName ,
		ISNULL(temp.PeakName,'(no highest peak)') , 
		ISNULL(temp.Elevation,0) ,
		ISNULL(temp.MountainRange,'(no mountain)') FROM
(SELECT  c.CountryName,p.PeakName ,p.Elevation ,m.MountainRange,
		DENSE_RANK () OVER (PARTITION BY CountryName ORDER BY Elevation DESC) AS [myRank]
		FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON
	c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains AS m ON
	mc.MountainId=m.Id
	LEFT JOIN Peaks AS p ON
	m.Id=p.MountainId) AS temp
	WHERE temp.myRank=1
	ORDER BY CountryName,PeakName




