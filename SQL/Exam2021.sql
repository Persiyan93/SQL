CREATE TABLE Users(
Id INT NOT NULL PRIMARY KEY IDENTITY,
Username VARCHAR(30) NOT NULL,
Password VARCHAR(30) NOT NULL,
Email VARCHAR(50) NOT NULL
)

CREATE TABLE Repositories(
Id INT NOT NULL PRIMARY KEY IDENTITY,
Name VARCHAR(50) NOT NULL
)

CREATE TABLE RepositoriesContributors(
RepositoryId INT NOT NULL REFERENCES Repositories(Id),
ContributorId INT NOT NULL REFERENCES Users(Id)
PRIMARY KEY(RepositoryId,ContributorId)
)

CREATE TABLE Issues(
Id INT NOT NULL PRIMARY KEY IDENTITY,
Title VARCHAR(255) NOT NULL,
IssueStatus CHAR(6) NOT NULL ,
RepositoryId INT NOT NULL REFERENCES Repositories(Id),
AssigneeId INT NOT NULL REFERENCES Users(Id)
)

CREATE TABLE Commits(
Id INT PRIMARY KEY IDENTITY,
Message VARCHAR(255) NOT NULL,
IssueId INT  REFERENCES Issues(Id),
RepositoryId INT NOT NULL REFERENCES Repositories(Id),
ContributorId INT NOT NULL REFERENCES Users(Id)
)

CREATE TABLE Files (
Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(100) NOT NULL,
Size DECIMAL(18,2) NOT NULL,
ParentId INT REFERENCES Files(Id),
CommitId INT NOT NULL REFERENCES Commits(Id)

)


--TASK 2

INSERT INTO Files(Name,Size,ParentId,CommitId)
VALUES
('Trade.idk',	2598.0	,1	,1),
('menu.net',	9238.31	,2,	2),
('Administrate.soshy',	1246.93,	3,	3),
('Controller.php',	7353.15	,4	,4),
('Find.java'	,9957.86,	5	,5),
('Controller.json',	14034.87,	3,	6),
('Operate.xix',	7662.92,	7	,7)

INSERT INTO Issues(Title,IssueStatus,RepositoryId,AssigneeId)
VALUES
('Critical Problem with HomeController.cs file',	'open',	1,	4),
('Typo fix in Judge.html',	'open',	4	,3),
('Implement documentation for UsersService.cs'	,'closed',	8,	2),
('Unreachable code in Index.cs'	,'open',	9,	8)

--TASK 3 
UPDATE  Issues
SET IssueStatus='closed'
WHERE AssigneeId=6

--TASK 4 
DELETE FROM RepositoriesContributors
 WHERE RepositoryId=3
DELETE FROM Issues
WHERE RepositoryId=3

--TASK 5
SELECT Id,Message,RepositoryId,ContributorId FROM Commits
ORDER BY Id,Message,RepositoryId,ContributorId

--TASK 6
SELECT Id,Name,Size FROM Files
WHERE Size>1000 AND Name LIKE ('%html%')
ORDER BY Size DESC,Id,Name

--TASK 7
SELECT i.id,u.Username +' : '+i.Title AS IssueAssignee FROM Issues AS i
JOIN Users AS u ON
i.AssigneeId=u.Id
ORDER BY i.Id DESC,i.AssigneeId

--TASK 8
SELECT f.Id ,f.Name,CONVERT(varchar(MAX),f.Size)+'KB' AS Size  FROM Files f
LEFT JOIN Files AS t ON
f.Id=t.ParentId
WHERE t.Id IS NULL
ORDER BY f.Id,f.Name,f.Size DESC

-- TASK 9
SELECT TOP(5)  r.Id,r.Name ,COUNT(*) AS Commits  FROM RepositoriesContributors AS rc
JOIN Repositories AS r ON
rc.RepositoryId=r.Id
JOIN Commits AS c ON
r.Id=c.Id
JOIN Commits c2 ON
c2.RepositoryId=rc.RepositoryId
GROUP BY r.Id,r.Name
ORDER BY Commits DESC,r.Id,r.Name


--TASK 10
SELECT u.Username AS Username ,AVG(f.Size) AS Size FROM Files AS f
JOIN Commits AS c ON
f.CommitId=c.Id
JOIN Users AS u ON
c.ContributorId=u.Id
GROUP BY u.Username
ORDER BY Size DESC,Username

--TASK 11
CREATE FUNCTION udf_AllUserCommits(@username VARCHAR(MAX)) 
RETURNS INT 
AS 
BEGIN 
DECLARE  @count INT;
 SET @count=(SELECT COUNT(*) FROM Users AS u
 JOIN Commits AS c ON
 u.Id=c.ContributorId
 WHERE u.Username=@username
 )
 RETURN @count
END

--TASK 12

CREATE PROCEDURE usp_SearchForFiles(@fileExtension VARCHAR(MAX))
AS 
BEGIN
SELECT Id,Name,CONVERT(varchar(MAX),Size)+'KB' AS Size FROM Files
WHERE  Name  LIKE('%.'+@fileExtension)


END