
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/16/2019 21:39:49
-- Generated from EDMX file: E:\vsproject\CarsSystem\Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Cars];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[CarsInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarsInfo];
GO
IF OBJECT_ID(N'[dbo].[ApplicationInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationInfo];
GO
IF OBJECT_ID(N'[dbo].[NewsInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GUID] nvarchar(64)  NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Pwd] nvarchar(64)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Gender] int  NOT NULL,
    [Phone] nvarchar(20)  NOT NULL,
    [Type] int  NOT NULL,
    [LoginDate] datetime  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [OfficeDate] datetime  NULL,
    [Department] nvarchar(100)  NULL,
    [Status] int  NOT NULL,
    [Mileage] int  NULL,
    [Total] int  NULL
);
GO

-- Creating table 'CarsInfo'
CREATE TABLE [dbo].[CarsInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GUID] nvarchar(64)  NOT NULL,
    [Number] nvarchar(100)  NOT NULL,
    [LicencePlate] nvarchar(50)  NOT NULL,
    [Seating] int  NOT NULL,
    [CarModel] nvarchar(100)  NOT NULL,
    [CarColor] nvarchar(50)  NOT NULL,
    [CarBrand] nvarchar(50)  NOT NULL,
    [OilWear] float  NOT NULL,
    [OilType] int  NOT NULL,
    [Mileage] int  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'ApplicationInfo'
CREATE TABLE [dbo].[ApplicationInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GUID] nvarchar(64)  NOT NULL,
    [Number] nvarchar(50)  NOT NULL,
    [AppUGUID] nvarchar(64)  NOT NULL,
    [AppLogin] nvarchar(50)  NOT NULL,
    [AppName] nvarchar(50)  NOT NULL,
    [DriverUGUID] nvarchar(64)  NOT NULL,
    [DriverLogin] nvarchar(50)  NOT NULL,
    [DriverName] nvarchar(50)  NOT NULL,
    [LeadUGUID] nvarchar(64)  NOT NULL,
    [LeadLogin] nvarchar(50)  NOT NULL,
    [LeadName] nvarchar(50)  NOT NULL,
    [CGUID] nvarchar(64)  NOT NULL,
    [LicencePlate] nvarchar(50)  NULL,
    [Reason] nvarchar(200)  NOT NULL,
    [Department] nvarchar(50)  NOT NULL,
    [Origin] nvarchar(50)  NOT NULL,
    [Distance] int  NOT NULL,
    [Destination] nvarchar(50)  NOT NULL,
    [GoDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [ApplicationDate] datetime  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'NewsInfo'
CREATE TABLE [dbo].[NewsInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GUID] nvarchar(64)  NOT NULL,
    [UGUID] nvarchar(64)  NOT NULL,
    [Title] nvarchar(200)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [EditDate] datetime  NOT NULL,
    [Type] int  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CarsInfo'
ALTER TABLE [dbo].[CarsInfo]
ADD CONSTRAINT [PK_CarsInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ApplicationInfo'
ALTER TABLE [dbo].[ApplicationInfo]
ADD CONSTRAINT [PK_ApplicationInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'NewsInfo'
ALTER TABLE [dbo].[NewsInfo]
ADD CONSTRAINT [PK_NewsInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------