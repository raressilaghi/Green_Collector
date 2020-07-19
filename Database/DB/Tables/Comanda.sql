CREATE TABLE [dbo].[Comanda]
(
	[Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
    [Town] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Number] INT NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [Date] DATETIME NULL, 
    [CustomerId] INT NULL, 
    CONSTRAINT [FK_Comanda_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id]),  
)
