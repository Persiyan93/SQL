--TASK 1

CREATE TABLE Logs (
LogId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
AccountId INT   REFERENCES Accounts(Id),
OldSum Money NOT NULL,
NewSum Money NOT NULL
)

CREATE OR ALTER  TRIGGER tr_CreateLog 
ON Accounts FOR UPDATE
AS
	INSERT INTO Logs(AccountId,OldSum,NewSum)
	SELECT i.id,d.Balance,i.Balance FROM  inserted AS i
	JOIN  deleted AS d ON
	d.Id=i.Id
GO

UPDATE Accounts
SET Balance=Balance-23
WHERE Id=1;

--TASK 2
CREATE TABLE NotificationEmails(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Recipient INT REFERENCES  Accounts(Id),
	[Subject] NVARCHAR(100),
	Body NVARCHAR(100)
)

CREATE OR ALTER  TRIGGER tr_CreateNotificationsWhenInsertInLogs
ON Logs FOR INSERT
AS
INSERT INTO NotificationEmails(Recipient,[Subject],Body)
SELECT i.AccountId,
		'Balance change for account: '+CONVERT(NVARCHAR,i.AccountId),
		'On ' +CONVERT(NVARCHAR ,GETDATE())+' your balance was changed from '+CONVERT(NVARCHAR,i.OldSum)+' to ' +CONVERT(NVARCHAR,i.NewSum)
  FROM inserted AS i

GO

UPDATE Accounts
SET Balance=Balance+2223
WHERE Id=1;


--TASK 3
CREATE OR ALTER PROC usp_DepositMoney(@accountId INT,@moneyAmount DECIMAL(20,4))
AS
	BEGIN TRANSACTION
	IF @moneyAmount<0 
		BEGIN
		ROLLBACK ;
		RETURN;
	END
	UPDATE Accounts 
		SET Balance=FORMAT (CONVERT(DECIMAL(20,4),(Balance+@moneyAmount)),'N4')
		WHERE Id=@accountId
COMMIT
GO

EXECUTE usp_DepositMoney 5,10