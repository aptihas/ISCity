
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/22/2017 23:47:22
-- Generated from EDMX file: C:\Users\macbookpro\documents\visual studio 2017\Projects\ISCity\ISCity\Models\DBISCityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBISCity];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Accounts_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_SubCompany_ManageCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubCompany] DROP CONSTRAINT [FK_SubCompany_ManageCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRoles_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoles_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRoles_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoles_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_ManageCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_ManageCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_SubCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_SubCompany];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[ManageCompany]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManageCompany];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[SubCompany]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubCompany];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [id] int IDENTITY(1,1) NOT NULL,
    [user_id] int  NOT NULL,
    [password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ManageCompany'
CREATE TABLE [dbo].[ManageCompany] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Name] nchar(20)  NOT NULL
);
GO

-- Creating table 'SubCompany'
CREATE TABLE [dbo].[SubCompany] (
    [id] int IDENTITY(1,1) NOT NULL,
    [mangeCompany_id] int  NOT NULL,
    [Name] nchar(10)  NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [id] int IDENTITY(1,1) NOT NULL,
    [user_id] int  NOT NULL,
    [role_id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [SecondName] nvarchar(50)  NOT NULL,
    [ThirdName] nvarchar(50)  NULL,
    [Street] nvarchar(50)  NOT NULL,
    [HouseNumber] nvarchar(10)  NOT NULL,
    [KorpusNumber] nvarchar(10)  NULL,
    [RoomNumber] nvarchar(10)  NULL,
    [Email] nvarchar(50)  NOT NULL,
    [EmailConfirm] bit  NOT NULL,
    [Telephone] nvarchar(15)  NULL,
    [ManageCompany_ID] int  NULL,
    [SubCompany_ID] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ManageCompany'
ALTER TABLE [dbo].[ManageCompany]
ADD CONSTRAINT [PK_ManageCompany]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'SubCompany'
ALTER TABLE [dbo].[SubCompany]
ADD CONSTRAINT [PK_SubCompany]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [user_id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK_Accounts_Users]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Accounts_Users'
CREATE INDEX [IX_FK_Accounts_Users]
ON [dbo].[Accounts]
    ([user_id]);
GO

-- Creating foreign key on [mangeCompany_id] in table 'SubCompany'
ALTER TABLE [dbo].[SubCompany]
ADD CONSTRAINT [FK_SubCompany_ManageCompany]
    FOREIGN KEY ([mangeCompany_id])
    REFERENCES [dbo].[ManageCompany]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubCompany_ManageCompany'
CREATE INDEX [IX_FK_SubCompany_ManageCompany]
ON [dbo].[SubCompany]
    ([mangeCompany_id]);
GO

-- Creating foreign key on [ManageCompany_ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_ManageCompany]
    FOREIGN KEY ([ManageCompany_ID])
    REFERENCES [dbo].[ManageCompany]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_ManageCompany'
CREATE INDEX [IX_FK_Users_ManageCompany]
ON [dbo].[Users]
    ([ManageCompany_ID]);
GO

-- Creating foreign key on [role_id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoles_Roles]
    FOREIGN KEY ([role_id])
    REFERENCES [dbo].[Roles]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoles_Roles'
CREATE INDEX [IX_FK_UserRoles_Roles]
ON [dbo].[UserRoles]
    ([role_id]);
GO

-- Creating foreign key on [SubCompany_ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_SubCompany]
    FOREIGN KEY ([SubCompany_ID])
    REFERENCES [dbo].[SubCompany]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_SubCompany'
CREATE INDEX [IX_FK_Users_SubCompany]
ON [dbo].[Users]
    ([SubCompany_ID]);
GO

-- Creating foreign key on [user_id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoles_Users]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoles_Users'
CREATE INDEX [IX_FK_UserRoles_Users]
ON [dbo].[UserRoles]
    ([user_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------