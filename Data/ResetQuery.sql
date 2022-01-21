DROP TABLE IF EXISTS [ChangeDates] 
DROP TABLE IF EXISTS [Issues]
DROP TABLE IF EXISTS [Contact_Info]
DROP TABLE IF EXISTS [Addresses]
DROP TABLE IF EXISTS [Customers]
DROP TABLE IF EXISTS [Administrators]

CREATE TABLE [Customers] (
  [Id] int NOT NULL IDENTITY (1, 1) ,
  [Name] nvarchar(50) NOT NULL ,
  [Email] nvarchar(100) NOT NULL UNIQUE,
 PRIMARY KEY ([Id])
) 
GO

CREATE TABLE [Issues] (
  [Id] int NOT NULL IDENTITY (1, 1) ,
  [Id_Customers] int NOT NULL ,
  [Subject] nvarchar(200) NOT NULL ,
  [Description] nvarchar(MAX) NOT NULL ,
  [Creation_Date] datetime NOT NULL ,
  [Last_Changed] datetime ,
 PRIMARY KEY ([Id])
) 
GO

CREATE TABLE [ChangeDates] (
  [Id] int NOT NULL IDENTITY (1, 1) ,
  [Id_Issues] int NOT NULL ,
  [CreatedOrChanged] datetime NOT NULL ,
 PRIMARY KEY ([Id])
) 
GO


CREATE TABLE [Contact_Info] (
  [Id] int NOT NULL IDENTITY (1, 1) ,
  [Id_Customers] int NOT NULL ,
  [Mobile] nvarchar(12) ,
  [Phone] nvarchar(12) , 
 PRIMARY KEY ([Id])
) 
GO

CREATE TABLE [Addresses] (
  [Id] int NOT NULL IDENTITY (1, 1) ,
  [Id_Customers] int NOT NULL ,
  [Adress_Line] nvarchar(200) NOT NULL ,
  [Postal_Code] nvarchar(8) NOT NULL,
  [City] nvarchar(20) NOT NULL ,
  [Country] nvarchar(20) , 
 PRIMARY KEY ([Id])
) 
GO


ALTER TABLE [ChangeDates] ADD FOREIGN KEY (Id_Issues) REFERENCES [Issues] ([Id]);
				
ALTER TABLE [Issues] ADD FOREIGN KEY (Id_Customers) REFERENCES [Customers] ([Id]);
				
ALTER TABLE [Contact_Info] ADD FOREIGN KEY (Id_Customers) REFERENCES [Customers] ([Id]);
				
ALTER TABLE [Addresses] ADD FOREIGN KEY (Id_Customers) REFERENCES [Customers] ([Id]);
				

