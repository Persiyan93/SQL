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
SELECT * FROM Employees AS e
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

--TASK 8   !!!!!!!!!!!!!!!!!!!!!

 SELECT  e.EmployeeID , e.FirstName,p.Name FROM Employees AS e
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


