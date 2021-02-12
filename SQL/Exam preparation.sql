	CREATE TABLE Vendors(
	VendorId INT PRIMARY KEY IDENTITY,
	Name VARCHAR(50) UNIQUE NOT NULL
	)

	CREATE TABLE Parts(
	PartId INT PRIMARY KEY IDENTITY,
	SerialNumber VARCHAR(50) UNIQUE NOT NULL,
	[Description] VARCHAR(255) ,
	Price DECIMAL(6,2)  NOT NULL,  
	VendorId INT REFERENCES Vendors(VendorId) NOT NULL ,
	StockQty INT DEFAULT 0 ,
	CONSTRAINT ck_CheckMoneyValue CHECK(Price >0 AND Price<9999.99) ,
	CONSTRAINT ck_StockQtyValueMustBePositive CHECK(StockQty >=0) 
	)
	CREATE TABLE Clients(
	ClientId INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50)  NOT NULL,
	LastName VARCHAR(50)  NOT NULL,
	Phone NCHAR(12) CHECK (LEN (Phone)=12)  NOT NULL
	)

	CREATE TABLE Mechanics(
	MechanicId INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50) UNIQUE NOT NULL,
	LastName VARCHAR(50) UNIQUE NOT NULL,
	Address VARCHAR(255) NOT NULL

	)
	CREATE TABLE Models(
	ModelId INT PRIMARY KEY IDENTITY,
	NAME VARCHAR(50) UNIQUE NOT NULL,

	)

	CREATE  TABLE Jobs(

	JobId INT PRIMARY KEY IDENTITY ,
	ModelId INT REFERENCES  Models(ModelId) NOT NULL,
	Status VARCHAR(11) DEFAULT 'Pending',
	ClientId INT REFERENCES Clients(ClientId),
	MechanicId INT REFERENCES Mechanics(MechanicId),
	IssueDate DATETIME NOT NULL,
	FinishDate DATETIME ,

	CONSTRAINT ck_AllowValues CHECK(Status IN ('Pending','In Progress','Finished')) 
	)




	CREATE TABLE Orders(
	OrderId INT PRIMARY KEY IDENTITY,
	JobId INT REFERENCES Jobs(JobId) ,
	IssueDate DATETIME ,
	Delivered  BIT DEFAULT 0
	)
	CREATE TABLE OrderParts(
	OrderId INT REFERENCES Orders(OrderId),
	PartId INT REFERENCES Parts(PartId),
	Quantity INT DEFAULT 1 

	PRIMARY KEY(OrderId,PartId),
	CONSTRAINT ck_QuantityMustBePositive CHECK(Quantity >0) 
	)
	CREATE TABLE PartsNeeded(
	JobId INT REFERENCES Jobs(JobId),
	PartId INT REFERENCES Parts(PartId) ,
	Quantity INT DEFAULT 1 
	PRIMARY KEY(JobId,PartId)
	)

 
 INSERT INTO Clients(FirstName,LastName,Phone)
	VALUES('Teri',	'Ennaco'	,'570-889-5187'),
		('Merlyn',	'Lawler'	,'201-588-7810'),
	('Georgene',	'Montezuma'	,'925-615-5185'),
    ('Jettie',	'Mconnell'	,'908-802-3564'),
	 ('Lemuel',	'Latzke'	,'631-748-6479'),
	('Melodie',	'Knipp'	,'805-690-1682'),
	('Candida',	'Corbley'	,'908-275-8357')
INSERT INTO Parts(SerialNumber,Description,Price,VendorId)
VALUES('WP8182119','Door Boot Seal','117.86','2'),
	('W10780048','Suspension Rod','42.81','1'),
	('W10841140','Silicone Adhesive ','6.77','4'),
	('WPY055980','High Temperature Adhesive','13.94','3')


--Task 3
UPDATE Jobs 

SET Status='In Progress' ,
	MechanicId=3


WHERE  Status='Pending'

--TASK 4
DELETE FROM OrderParts WHERE OrderId=19
DELETE FROM Orders
WHERE OrderId=19

--Task 5
SELECT m.FirstName+' '+m.LastName AS [Mechanic],j.Status,FORMAT(j.IssueDate,'yyyy-mm-dd') AS IssueDate FROM jobs as j
JOIN Mechanics as m ON
j.MechanicId=m.MechanicId
ORDER BY m.MechanicId ,IssueDate,j.MechanicId

--TASK 6
SELECT c.FirstName+' '+c.LastName AS Client,DATEDIFF(DAY,j.IssueDate, CONVERT(DATE,'2017-04-24') )AS [Days Going ] ,j.Status FROM Jobs AS j
JOIN Clients AS c  ON
j.ClientId=c.ClientId
WHERE j.Status  !=  'Finished'
ORDER BY [Days Going ] DESC ,j.ClientId 

--TASK 7 

SELECT m.FirstName+' '+ m.LastName AS Mechanic  ,AVG(DATEDIFF(DAY,IssueDate,FinishDate)) FROM Jobs as j
JOIN Mechanics as m ON
j.MechanicId=m.MechanicId
GROUP BY m.FirstName,m.LastName,m.MechanicId
ORDER BY m.MechanicId

--Task 8
SELECT  m.FirstName+' '+m.LastName   AS Available FROM Jobs AS j
RIGHT JOIN Mechanics AS m ON
j.MechanicId=m.MechanicId
WHERE j.JobId IS NULL OR j.JobId  NOT IN (SELECT j.JobId FROM Jobs AS j
					WHERE j.Status!='Finished')

GROUP BY m.FirstName+' '+m.LastName,m.MechanicId
ORDER BY m.MechanicId 

--TASK 9
SELECT j.JobId ,ISNULL(SUM(Price*Quantity),0) AS TOTAL FROM Jobs as j
 LEFT JOIN Orders AS o ON
j.JobId=o.JobId
LEFT JOIN OrderParts AS op ON
o.OrderId=op.OrderId
LEFT JOIN Parts AS p ON 
p.PartId=op.PartId
WHERE j.Status='Finished'
GROUP BY j.JobId
ORDER BY Total DESC,JobId

--TASK 10
--SELECT 
--		op.PartId 
--		,[Description]
--		,[Required]
--		,[In Stock]
--		,CASE 
--			WHEN Delivered=0 THEN Quantity
--			WHEN Delivered IS NULL THEN 0
--			END AS Ordered
--			FROM 
--(SELECT t.PartId,p.Description,t.Required,p.StockQty AS [In Stock] FROM 
--	(SELECT 
--			pn.PartId 
--			,p.StockQty
--			,SUM(pn.Quantity) AS [Required]
--			,SUM(op.Quantity) AS Ordered

--		FROM Jobs AS j 
--		JOIN PartsNeeded AS pn ON
--		j.JobId=pn.JobId
--		JOIN OrderParts as op ON 
--		pn.PartId=op.PartId
--		JOIN Parts as p ON
--		pn.PartId=p.PartId
		
--	WHERE j.Status!='Finished' AND 
	
--	GROUP BY pn.PartId ,p.StockQty   ) as t 
--	JOIN Parts AS p ON
--		t.PartId=p.PartId

--WHERE t.Required>p.StockQty) as m

-- LEFT JOIN (SELECT * FROM Orders
--		WHERE Delivered=0)  AS o ON
-- o.OrderId=op.OrderId
 
	
	--TASK 11
	
	


CREATE OR ALTER  PROCEDURE  usp_PlaceOrder (@jobId INT ,@serialNumber VARCHAR(50),@quantity INT ) 
AS

DECLARE @status VARCHAR(11);
SET @status=(SELECT  Status FROM Jobs WHERE JobId=@jobId )
IF(@status='Finished')
	BEGIN
	THROW  50011,'The job is not active!',1;
	END



GO