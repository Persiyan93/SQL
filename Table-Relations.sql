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