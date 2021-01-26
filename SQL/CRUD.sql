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


SELECT FirstName +' ' + MiddleName + ' ' + LastName AS [Full Name] FROM Employees WHERE
	Salary IN (25000 ,14000);

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

CREATE VIEW v_EmployeeNameJobTitle AS
	SELECT FirstName +' ' + ISNULL(MiddleName,'')+ ' ' + LastName AS[Full Name] ,JobTitle FROM Employees;


SELECT DISTINCT JobTitle FROM Employees;

SELECT TOP(10) * FROM Projects ORDER BY StartDate,[Name];

SELECT TOP(7) FirstName,LastName,HireDate FROM Employees 
	ORDER BY HireDate DESC

UPDATE Employees
	SET Salary=Salary*1.2
	WHERE DepartmentID IN(12,4,46,42)

USE Geography
SELECT PeakName FROM Peaks 
	ORDER BY PeakName

SELECT CountryName , [Population] FROM Countries
	WHERE ContinentCode='EU'
	ORDER BY Population DESC,CountryName

SELECT CountryName,CountryCode,
	CASE
		WHEN CurrencyCode='EUR' THEN 'Euro'
		ELSE 'Not Euro' 
	END
	AS Currency FROM Countries
	ORDER BY CountryName