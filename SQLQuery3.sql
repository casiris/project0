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

SELECT * FROM Users;

INSERT INTO Accounts VALUES ('savings',100,1);

SELECT * FROM Accounts;