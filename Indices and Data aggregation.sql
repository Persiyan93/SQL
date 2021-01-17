-- TASK 1
SELECT COUNT(*) AS Count FROM WizzardDeposits

--TASK 2 
SELECT MAX(MagicWandSize) AS LongestMagicWand FROM WizzardDeposits 

--TASK 3 
SELECT  DepositGroup,MAX(MagicWandSize) AS LongestMagicWand 
FROM WizzardDeposits 

GROUP BY (DepositGroup)

--TASK 4 
SELECT TOP 2	  DepositGroup
FROM WizzardDeposits 
GROUP BY (DepositGroup)
ORDER BY AVG(MagicWandSize)

--TAST 5
SELECT  DepositGroup  ,SUM(DepositAmount) AS TotalSUM 
	FROM WizzardDeposits
GROUP BY (DepositGroup)
	
--TASK 6
SELECT DepositGroup ,SUM(DepositAmount) AS TotalSUM FROM WizzardDeposits
WHERE MagicWandCreator = ('Ollivander family')
GROUP BY (DepositGroup)

--TASK 7
SELECT DepositGroup ,SUM(DepositAmount) AS TotalSum FROM WizzardDeposits
WHERE MagicWandCreator = ('Ollivander family')
GROUP BY (DepositGroup)
HAVING  SUM(DepositAmount) <150000
ORDER BY TotalSum DESC

--TASK 8 
SELECT DepositGroup , MagicWandCreator,MIN(DepositCharge) AS MinDepositCharge 
FROM WizzardDeposits
GROUP BY DepositGroup , MagicWandCreator
ORDER BY MagicWandCreator,DepositGroup

--TASK 9
SELECT AgeRange ,COUNT(*)
FROM 
(SELECT CASE 
		WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
		WHEN Age BETWEEN 11	 AND 20 THEN '[11-20]'
		WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
		WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
		WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
		WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
		WHEN Age >60 THEN '[61+]'
		END AS AgeRange  
		FROM WizzardDeposits ) AS AgeRange

GROUP BY (AgeRange)


--TASK 10 
SELECT FirstLetter FROM
	(SELECT  LEFT(FirstName,1) AS FirstLetter,DepositGroup FROM WizzardDeposits
	WHERE DepositGroup ='Troll Chest' ) AS FirstLetter
	GROUP BY FirstLetter
	ORDER BY FirstLetter

--TASK 11
SELECT DepositGroup ,IsDepositExpired,AVG(DepositInterest) AS AverageInterest FROM WizzardDeposits
WHERE DepositExpirationDate>'1985-01-01'
GROUP BY DepositGroup ,IsDepositExpired 
ORDER BY DepositGroup DESC,IsDepositExpired

--TASK 12


--TASK 13
SELECT DepartmentID ,SUM(Salary) AS TotalSalary FROM Employees
GROUP BY DepartmentID
ORDER BY DepartmentID

--TASK 14
SELECT DepartmentID  , MIN(Salary) AS MinimumSalary FROM Employees
WHERE DepartmentID IN(2,5,7) AND HireDate>'2000-01-01'
GROUP BY DepartmentID
ORDER BY DepartmentID

--TASK 15
SELECT * 
INTO #TempEmployes
FROM Employees
WHERE Salary>30000
DELETE  FROM #TempEmployes
WHERE ManagerID=42
UPDATE #TempEmployes 
SET Salary=Salary+5000
WHERE DepartmentID=1
SELECT  DepartmentID ,AVG(Salary) AS AverageSalary FROM #TempEmployes
GROUP BY DepartmentID

--TASK 16
SELECT DepartmentID,MAX(Salary) AS MaxSalary FROM Employees
GROUP BY  DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

--TASK 17
SELECT COUNT(*) AS COUNT FROM Employees
WHERE ManagerID IS NULL

--TASK 18 
SELECT DepartmentID ,Salary FROM (
SELECT *,ROW_NUMBER () OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS s FROM Employees) AS OrderEmployees
WHERE OrderEmployees.s =3

-- TASK 19

SELECT TOP(10) FirstName,LastName,e.DepartmentID FROM Employees   AS e
JOIN 
(SELECT DepartmentID ,AVG(Salary) AS AverageSalary FROM Employees
GROUP BY DepartmentID ) AS s
ON e.DepartmentID=s.DepartmentID
WHERE e.Salary>s.AverageSalary
ORDER BY DepartmentID
