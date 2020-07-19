﻿/*
Deployment script for Database

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Database"
:setvar DefaultFilePrefix "Database"
:setvar DefaultDataPath "C:\Users\HP\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\"
:setvar DefaultLogPath "C:\Users\HP\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[Order].[Data] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Order])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Rename refactoring operation with key c3e587f6-9ff9-4259-b374-701bd5a70d20 is skipped, element [dbo].[WasteOrder].[Id] (SqlSimpleColumn) will not be renamed to OrderId';


GO
PRINT N'Starting rebuilding table [dbo].[Order]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Order] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Address]     NVARCHAR (MAX) NOT NULL,
    [PhoneNumber] NVARCHAR (50)  NOT NULL,
    [Date]        DATE           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Order])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Order] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Order] ([Id], [Address], [PhoneNumber])
        SELECT   [Id],
                 [Address],
                 [PhoneNumber]
        FROM     [dbo].[Order]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Order] OFF;
    END

DROP TABLE [dbo].[Order];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Order]', N'Order';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Waste]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Waste] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [TypeName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Waste])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Waste] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Waste] ([Id], [TypeName])
        SELECT   [Id],
                 [TypeName]
        FROM     [dbo].[Waste]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Waste] OFF;
    END

DROP TABLE [dbo].[Waste];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Waste]', N'Waste';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[WasteOrder]...';


GO
CREATE TABLE [dbo].[WasteOrder] (
    [OrderId]  INT NOT NULL,
    [TypeId]   INT NOT NULL,
    [Quantity] INT NOT NULL
);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'c3e587f6-9ff9-4259-b374-701bd5a70d20')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('c3e587f6-9ff9-4259-b374-701bd5a70d20')

GO

GO
PRINT N'Update complete.';


GO
