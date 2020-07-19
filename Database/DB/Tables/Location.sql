CREATE TABLE [dbo].[Location]
(
	[OrderId] INT NOT NULL, 
    [TypeId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Address] NVARCHAR(MAX) NOT NULL, 
    [Longitude] NVARCHAR(MAX) NOT NULL, 
    [Latitude] NVARCHAR(MAX) NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [Collected] NVARCHAR(50) NULL, 
    [ClientName] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Location_Order] FOREIGN KEY ([OrderId]) REFERENCES [Comanda]([Id]), 
    CONSTRAINT [FK_Location_Waste] FOREIGN KEY ([TypeId]) REFERENCES [Waste]([Id]) 
)
