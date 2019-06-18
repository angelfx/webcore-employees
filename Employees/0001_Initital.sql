IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Departments] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(max) NULL,
    [TelNo] nvarchar(max) NULL,
    [DepartmentId] int NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Employees_DepartmentId] ON [Employees] ([DepartmentId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190617050939_Initial', N'2.2.4-servicing-10062');

GO

