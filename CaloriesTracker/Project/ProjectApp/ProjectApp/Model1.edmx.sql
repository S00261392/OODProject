
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/10/2024 19:24:54
-- Generated from EDMX file: C:\Users\minni\ATU\OOD\Project\Project\ProjectApp\ProjectApp\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [food];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FoodTypeSet'
CREATE TABLE [dbo].[FoodTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [FoodTypeImage] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FoodItemSet'
CREATE TABLE [dbo].[FoodItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Calories] int  NOT NULL,
    [FoodItemImage] nvarchar(max)  NOT NULL,
    [FoodTypeId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'FoodTypeSet'
ALTER TABLE [dbo].[FoodTypeSet]
ADD CONSTRAINT [PK_FoodTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FoodItemSet'
ALTER TABLE [dbo].[FoodItemSet]
ADD CONSTRAINT [PK_FoodItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FoodTypeId] in table 'FoodItemSet'
ALTER TABLE [dbo].[FoodItemSet]
ADD CONSTRAINT [FK_FoodTypeFoodItem]
    FOREIGN KEY ([FoodTypeId])
    REFERENCES [dbo].[FoodTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FoodTypeFoodItem'
CREATE INDEX [IX_FK_FoodTypeFoodItem]
ON [dbo].[FoodItemSet]
    ([FoodTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------