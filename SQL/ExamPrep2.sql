CREATE TABLE Cities(
Id INT PRIMARY KEY IDENTITY NOT NULL ,
Name NVARCHAR(20) NOT NULL,
CountryCode CHAR(2)  NOT NULL
)

CREATE TABLE Hotels(
Id INT PRIMARY KEY IDENTITY NOT NULL 
,Name NVARCHAR(30) NOT NULL,
CityId INT NOT NULL REFERENCES Cities(Id) ,
EmployeeCount INT NOT NULL ,
BaseRate DECIMAL(18,2) 
)

CREATE TABLE Rooms(
Id INT PRIMARY KEY  IDENTITY NOT NULL ,
Price DECIMAL(18,2) NOT NULL,
Type NVARCHAR(20) NOT NULL,
Beds INT  NOT NULL,
HotelId INT NOT NULL REFERENCES Hotels(Id) 
)

CREATE TABLE Trips(
Id INT PRIMARY KEY IDENTITY NOT NULL ,
RoomId INT REFERENCES Rooms(Id) NOT NULL,
BookDate DATE NOT NULL ,
ArrivalDate DATE NOT NULL ,
ReturnDate DATE NOT NULL ,
CancelDate DATE,
CHECK(BookDate<ArrivalDate),
CHECK(ArrivalDate<ReturnDate)

)

CREATE TABLE Accounts(
Id INT PRIMARY KEY  IDENTITY NOT NULL ,
FirstName NVARCHAR(50) NOT NULL,
MiddleName NVARCHAR(20) ,
LastName NVARCHAR(50),
CityId INT NOT NULL REFERENCES Cities(Id) ,
BirthDate DATE NOT NULL,
Email VARCHAR(100) NOT NULL UNIQUE,
)

CREATE TABLE AccountsTrips(
AccountId INT  NOT NULL REFERENCES Accounts(Id) ,
TripId INT NOT NULL REFERENCES Trips(Id) ,
Luggage INT NOT NULL CHECK(Luggage>=0),
PRIMARY KEY(AccountId,TripId)
)


--TASK 11
CREATE  FUNCTION udF_GetAvailableRoom(@HotelId INT,@Date DATE ,@people INT)
RETURNS VARCHAR(MAX)
AS 
BEGIN
DECLARE @roomInfo VARCHAR(MAX)= (SELECT TOP(1) 'Room ' + CONVERT(VARCHAR,r.Id)
								+': '+r.Type+ ' ('+CONVERT(VARCHAR,r.Beds)+' beds) - $' +CONVERT(VARCHAR,(h.BaseRate+r.Price)* @people) 
								FROM Trips AS t
RIGHT JOIN Rooms as r ON 
t.RoomId=r.Id
JOIN Hotels as h ON
r.HotelId=h.Id


WHERE HotelId=@HotelId AND  r.Beds>=2 
	AND RoomId NOT IN  (SELECT RoomId FROM Trips 
			WHERE @Date BETWEEN ArrivalDate AND ReturnDate AND CancelDate IS NULL )
	
ORDER BY r.Price DESC)
IF(@roomInfo IS NULL)
RETURN 'No rooms available'
RETURN @roomInfo

END
SELECT dbo.udf_GetAvailableRoom(94, '2015-07-26', 3)
--TASK 10
SELECT t.Id,a.FirstName+' '+ISNULL(MiddleName+' ','')+a.LastName AS [Full Name] ,c.Name AS [FROM] ,hc.Name AS [To],
				DATEDIFF(DAY,t.ArrivalDate,t.ReturnDate ) AS Duration				
FROM AccountsTrips AS at
JOIN Accounts AS a ON
at.AccountId=a.Id
JOIN  Cities AS c ON
a.CityId=c.Id
JOIN Trips AS t ON
at.TripId=t.Id
JOIN Rooms AS r ON
t.RoomId=r.Id
JOIN Hotels  AS h ON 
h.Id=r.HotelId
JOIN Cities AS hc ON
h.CityId=hc.Id
 





				