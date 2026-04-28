IF NOT EXISTS (SELECT 1 FROM sys.sql_logins WHERE name = 'admin')
BEGIN
  CREATE LOGIN admin WITH PASSWORD = 'Admin123!';
END
GO

IF DB_ID('persona_db') IS NULL
BEGIN
  CREATE DATABASE persona_db;
END
GO

USE persona_db;
GO

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'admin')
BEGIN
  CREATE USER admin FOR LOGIN admin;
  EXEC sp_addrolemember 'db_owner', 'admin';
END
GO

IF OBJECT_ID('dbo.Persona', 'U') IS NULL
BEGIN
  CREATE TABLE dbo.Persona (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NULL,
    FechaNacimiento DATE NULL
  );
END
GO
