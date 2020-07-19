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
The column [dbo].[Comanda].[CustomerId] on table [dbo].[Comanda] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Comanda])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Customer].[IdOrder] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Customer])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Rename refactoring operation with key b0d617b9-0c10-4bf4-b399-8477ce9f6f4e is skipped, element [dbo].[FK_Comanda_ToTable] (SqlForeignKeyConstraint) will not be renamed to [FK_Comanda_Customer]';


GO
PRINT N'Altering [dbo].[Comanda]...';


GO
ALTER TABLE [dbo].[Comanda]
    ADD [CustomerId] INT NOT NULL;


GO
PRINT N'Altering [dbo].[Customer]...';


GO
ALTER TABLE [dbo].[Customer] DROP COLUMN [IdOrder];


GO
PRINT N'Creating [dbo].[FK_Comanda_Customer]...';


GO
ALTER TABLE [dbo].[Comanda] WITH NOCHECK
    ADD CONSTRAINT [FK_Comanda_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id]);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'b0d617b9-0c10-4bf4-b399-8477ce9f6f4e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('b0d617b9-0c10-4bf4-b399-8477ce9f6f4e')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Comanda] WITH CHECK CHECK CONSTRAINT [FK_Comanda_Customer];


GO
PRINT N'Update complete.';


GO
