USE SoftUni
GO	
SELECT * FROM Departments;

SELECT [NAME] FROM Departments;

SELECT FirstName,LastName,Salary FROM Employees;

SELECT FirstName,MiddleName,LastName FROM Employees;

SELECT FirstName +'.'+LastName+'@softuni.bg' AS [Full Email Adress] FROM Employees;

SELECT DISTINCT Salary FROM Employees;  

SELECT * FROM Employees WHERE JobTitle LIKE'Sales Representative';

SELECT FirstName , LastName , JobTitle FROM Employees WHERE Salary BETWEEN 20000 AND 30000; 

--TASK 10
SELECT FirstName +' ' + MiddleName + ' ' + LastName AS [Full Name] FROM Employees WHERE
	Salary IN (25000 ,14000,12500,23600);

SELECT FirstName ,LastName FROM Employees WHERE
	ManagerID IS NULL;

SELECT FirstName ,LastName ,Salary FROM Employees
		WHERE Salary>50000 
		ORDER BY Salary DESC 

SELECT TOP(5) FirstName ,LastName  FROM Employees
		WHERE Salary>50000 
		ORDER BY Salary DESC 

SELECT FirstName, LastName FROM Employees
	Where NOT (DepartmentID=4);

SELECT * FROM Employees		
	ORDER BY Salary DESC,FirstName,LastName DESC,MiddleName;
--TASK 16
CREATE VIEW V_EmployeesSalaries AS
 SELECT  e.FirstName,e.LastName,e.Salary  FROM Employees as e
 --TASK 17
CREATE VIEW v_EmployeeNameJobTitle AS
	SELECT FirstName +' ' + ISNULL(MiddleName,'')+ ' ' + LastName AS[Full Name] ,JobTitle FROM Employees;


SELECT DISTINCT JobTitle FROM Employees;

SELECT TOP(10) * FROM Projects ORDER BY StartDate,[Name];

SELECT TOP(7) FirstName,LastName,HireDate FROM Employees 
	ORDER BY HireDate DESC
--TASK 21
	UPDATE e 
	SET e.Salary=e.Salary*1.12
	FROM Employees AS  e
	JOIN  Departments AS d ON
	e.DepartmentId=d.DepartmentID
	WHERE d.Name='Engineering' OR d.Name='Tool Design' OR d.Name='Marketing' OR d.Name='Information Services'
	SELECT Salary FROM Employees


	
	
--TASK 22
USE Geography
SELECT PeakName FROM Peaks 
	ORDER BY PeakName
--TASK 23
SELECT TOP(30) c.CountryName,c.Population FROM Countries AS c
	WHERE c.ContinentCode='EU'
	ORDER BY c.Population DESC,c.CountryName
--TASK 24
SELECT CountryName,CountryCode,
	CASE
		WHEN CurrencyCode='EUR' THEN 'Euro'
		ELSE 'Not Euro' 
	END
	AS Currency FROM Countries
	ORDER BY CountryName


--TASK 25
SELECT Name FROM Characters
ORDER BY Name

