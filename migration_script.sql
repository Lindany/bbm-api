IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230806174948_mssql.local_migration_734')
BEGIN
    CREATE TABLE [Church] (
        [churchId] int NOT NULL IDENTITY,
        [churchName] nvarchar(max) NULL,
        [location] nvarchar(max) NOT NULL,
        [branch] nvarchar(max) NOT NULL,
        [province] nvarchar(max) NOT NULL,
        [city] nvarchar(max) NOT NULL,
        [region] nvarchar(max) NOT NULL,
        [pastorId] int NOT NULL,
        CONSTRAINT [PK_Church] PRIMARY KEY ([churchId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230806174948_mssql.local_migration_734')
BEGIN
    CREATE TABLE [Leader] (
        [leaderId] int NOT NULL IDENTITY,
        [ministry] nvarchar(max) NOT NULL,
        [office] nvarchar(max) NOT NULL,
        [personId] int NOT NULL,
        CONSTRAINT [PK_Leader] PRIMARY KEY ([leaderId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230806174948_mssql.local_migration_734')
BEGIN
    CREATE TABLE [Person] (
        [personId] int NOT NULL IDENTITY,
        [address] nvarchar(max) NOT NULL,
        [comments] nvarchar(max) NOT NULL,
        [contactNumber] nvarchar(max) NOT NULL,
        [gender] int NOT NULL,
        [maritalStatus] nvarchar(max) NOT NULL,
        [name] nvarchar(max) NOT NULL,
        [surname] nvarchar(max) NOT NULL,
        [churchId] int NOT NULL,
        CONSTRAINT [PK_Person] PRIMARY KEY ([personId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230806174948_mssql.local_migration_734')
BEGIN
    CREATE TABLE [Stats] (
        [statsId] int NOT NULL IDENTITY,
        [adult] int NOT NULL,
        [car] int NOT NULL,
        [fk] int NOT NULL,
        [saved] int NOT NULL,
        [offering] real NOT NULL,
        [visitors] int NOT NULL,
        [date] datetime2 NOT NULL,
        [churchId] int NOT NULL,
        CONSTRAINT [PK_Stats] PRIMARY KEY ([statsId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230806174948_mssql.local_migration_734')
BEGIN
    CREATE TABLE [User] (
        [userId] int NOT NULL IDENTITY,
        [role] int NOT NULL,
        [username] int NOT NULL,
        [password] int NOT NULL,
        [personId] int NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([userId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230806174948_mssql.local_migration_734')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230806174948_mssql.local_migration_734', N'7.0.9');
END;
GO

COMMIT;
GO

