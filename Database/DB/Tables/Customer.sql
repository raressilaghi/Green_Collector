CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,    
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(50) NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(MAX) NOT NULL, 
    [UniqueCodeAccess] NVARCHAR(MAX) NOT NULL, 
    [QuantityColected] INT NULL, 
    [IdOrder] INT NULL, 
   
)
