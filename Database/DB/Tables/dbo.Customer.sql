CREATE TABLE [dbo].[Customer] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (50)  NOT NULL,
    [LastName]         NVARCHAR (50)  NOT NULL,
    [EmailAddress]     NVARCHAR (50)  NOT NULL,
    [PhoneNumber]      NVARCHAR (50)  NOT NULL,
    [Address]          NVARCHAR (MAX) NOT NULL,
    [UniqueCodeAccess] NVARCHAR (MAX) NOT NULL,
    [QuantityColected] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

