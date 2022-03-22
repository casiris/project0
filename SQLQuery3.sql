CREATE DATABASE bankingDB;

USE bankingDB;

CREATE TABLE Users
(
	id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	firstName VARCHAR(20),
	lastName VARCHAR(20),
	username VARCHAR(20),
	password VARCHAR(50)
);

CREATE TABLE Accounts
(
	accountNumber INT PRIMARY KEY IDENTITY(1000, 5) NOT NULL,
	accountTYPE VARCHAR(8),
	accountBalance FLOAT,
	userID INT REFERENCES Users(id)
);

INSERT INTO Users VALUES ('david', 'acuff', 'd', 'a');
INSERT INTO Users VALUES ('Jane', 'Doe', 'death', 'rattle');

SELECT * FROM Users;
SELECT * FROM Users WHERE username = 'username' AND password = 'password';

INSERT INTO Accounts VALUES ('savings',5000,3);

SELECT * FROM Accounts;
SELECT IDENT_CURRENT('Accounts.accountNumber') AS currentID;
SELECT SCOPE_IDENTITY() AS currentID;

SELECT accountNumber FROM Accounts WHERE accountNumber = 1000;
SELECT * FROM Accounts WHERE userID = 1;

UPDATE Accounts SET accountBalance = accountBalance + 2000 WHERE accountNumber = 1025;