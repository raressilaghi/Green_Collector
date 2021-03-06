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
PRINT N'Creating [dbo].[FK_Location_Order]...';


GO
ALTER TABLE [dbo].[Location] WITH NOCHECK
    ADD CONSTRAINT [FK_Location_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Comanda] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Location_Waste]...';


GO
ALTER TABLE [dbo].[Location] WITH NOCHECK
    ADD CONSTRAINT [FK_Location_Waste] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[Waste] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Location] WITH CHECK CHECK CONSTRAINT [FK_Location_Order];

ALTER TABLE [dbo].[Location] WITH CHECK CHECK CONSTRAINT [FK_Location_Waste];


GO
PRINT N'Update complete.';


GO
