CREATE TABLE Persons(
PersonId INT PRIMARY KEY,
FirstName VARCHAR(50),
Salary FLOAT ,
PassportId INT
)

CREATE TABLE Passports(
PassportId INT PRIMARY KEY,
PassportNumber VARCHAR(50) UNIQUE,
)

ALTER TABLE Persons
ADD CONSTRAINT FK_Persons_Passports FOREIGN KEY
(PassportId) REFERENCES Passports(PassportId)

--2 TASK
CREATE TABLE Models(
ModelId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50),
ManufacturerId INT
)
CREATE TABLE Manufacturers(
ManufacturerId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50) ,
EstablishedOn DATE
)
ALTER TABLE Models
ADD CONSTRAINT FK_Models_Manufacturers FOREIGN KEY
(ManufacturerId) REFERENCES Manufacturers(ManufacturerId)

--3 TASK
CREATE TABLE Students(
StudentId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50),
)
CREATE TABLE Exams(
ExamId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50) ,
)
CREATE TABLE StudentsExams(
StudentId INT ,
ExamId INT,
CONSTRAINT FK_SudentsExams_Students FOREIGN KEY
(StudentId) REFERENCES Students(StudentId),
CONSTRAINT FK_SudentsExams_Exams FOREIGN KEY
(ExamId) REFERENCES Exams(ExamId),
PRIMARY KEY(StudentId,ExamId)

)

--4 TASK
CREATE TABLE Teachers(
TeacherId INT PRIMARY KEY IDENTITY(1,1) ,
[Name] VARCHAR(50),
ManagerId INT
CONSTRAINT FK_Teachers_Teachers FOREIGN KEY
(ManagerId) REFERENCES Teachers(TeacherId),
)

--5 Task
CREATE TABLE Cities(
CityId INT PRIMARY KEY IDENTITY(1,1) ,
[Name] VARCHAR(50),

)
 CREATE TABLE Customers(
CustomerId INT PRIMARY KEY IDENTITY(1,1) ,
[Name] VARCHAR(50),
Birthday DATETIME2,
CityId INT,
CONSTRAINT FK_Customers_Cities FOREIGN KEY
(CityId) REFERENCES Cities(CityId),
)
CREATE TABLE Orders(
OrderId INT PRIMARY KEY IDENTITY(1,1) ,
CustomerId INT,
CONSTRAINT FK_Orders_Custumers FOREIGN KEY
(CustomerId) REFERENCES Customers(CustomerID),
)


 CREATE TABLE ItemTypes(
ItemTypeId INT PRIMARY KEY IDENTITY(1,1) ,
[Name] VARCHAR(50),
)

 CREATE TABLE Items(
ItemId INT PRIMARY KEY IDENTITY(1,1) ,
[Name] VARCHAR(50),
ItemTypeId INT
CONSTRAINT FK_Items_ItemTypes FOREIGN KEY
(ItemTypeId) REFERENCES ItemTypes(ItemTypeId),
)

ALTER TABLE OrderItems
ADD CONSTRAINT FK_OrderItems_Items FOREIGN  KEY
(ItemId) REFERENCES Items(ItemId)

 
 --TASK 6
  CREATE TABLE Subjects(
SubjectId INT PRIMARY KEY IDENTITY(1,1) ,
[SubjectName] VARCHAR(50),
)
  CREATE TABLE Majors(
MajorId INT PRIMARY KEY IDENTITY(1,1) ,
[Name] VARCHAR(50),
)
CREATE TABLE Students(
StudentId INT PRIMARY KEY IDENTITY(1,1) ,
StudentNumber  INT UNIQUE,
StudentName VARCHAR(50),
MajorId INT
CONSTRAINT FK_Students_Majors FOREIGN KEY
(MajorId) REFERENCES Majors(MajorId)
)
CREATE TABLE Payments(
PaymentId INT PRIMARY KEY IDENTITY(1,1) ,
PaymentDate  DATETIME2,
PaymentAmount FLOAT,
StudentId INT
CONSTRAINT FK_Payments_Students FOREIGN KEY
(StudentId) REFERENCES Students(StudentNumber)
)
  CREATE TABLE Agenda(
StudentId INT  ,
SubjectId INt
CONSTRAINT FK_Agenda_Students FOREIGN KEY
(StudentId) REFERENCES Students(StudentId),

CONSTRAINT FK_Agenda_Subjects FOREIGN KEY
(SubjectId) REFERENCES Subjects(SubjectId),
)

ALTER TABLE Agenda
	ALTER COLUMN StudentId INT NOT NULL 
	ALTER TABLE Agenda
	ALTER COLUMN SubjectId INT NOT NULL
	ALTER TABLE Agenda
ADD PRIMARY KEY(StudentId,SubjectId)


 --TASK 9

 SELECT [MountainRange],[PeakName],[Elevation] FROM Peaks
 JOIN Mountains ON
 Mountains.Id=Peaks.MountainId
 WHERE MountainRange='Rila'
 ORDER BY Elevation DESC