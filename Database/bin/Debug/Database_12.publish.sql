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
PRINT N'Starting rebuilding table [dbo].[Location]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Location] (
    [OrderId]   INT            NOT NULL,
    [TypeId]    INT            NOT NULL,
    [Quantity]  INT            NOT NULL,
    [Address]   NVARCHAR (MAX) NOT NULL,
    [Longitude] NVARCHAR (MAX) NOT NULL,
    [Latitude]  NVARCHAR (MAX) NOT NULL,
    [Date]      DATETIME       NOT NULL
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Location])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Location] ([OrderId], [TypeId], [Quantity], [Address], [Longitude], [Latitude], [Date])
        SELECT [OrderId],
               [TypeId],
               [Quantity],
               [Address],
               [Longitude],
               [Latitude],
               [Date]
        FROM   [dbo].[Location];
    END

DROP TABLE [dbo].[Location];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Location]', N'Location';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Update complete.';


GO
