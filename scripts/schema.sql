-- =============================================================
-- schema.sql — DDL idempotente para persona_db
-- Ejecutado por init-db.sh en cada arranque del contenedor `db`.
-- Fuente de verdad alineada con personapi-dotnet/Database/ddl.sql
-- =============================================================

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

-- profesion --------------------------------------------------
IF OBJECT_ID('dbo.profesion', 'U') IS NULL
BEGIN
  CREATE TABLE dbo.profesion (
    id  INT          IDENTITY(1,1) NOT NULL,
    nom VARCHAR(90)  NOT NULL,
    des VARCHAR(MAX) NULL,
    CONSTRAINT PK_profesion PRIMARY KEY (id)
  );
END
GO

-- persona ----------------------------------------------------
IF OBJECT_ID('dbo.persona', 'U') IS NULL
BEGIN
  CREATE TABLE dbo.persona (
    cc       BIGINT      NOT NULL,
    nombre   VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    genero   CHAR(1)     NULL,
    edad     INT         NULL,
    CONSTRAINT PK_persona     PRIMARY KEY (cc),
    CONSTRAINT CK_persona_gen CHECK (genero IN ('M','F'))
  );
END
GO

-- estudios ---------------------------------------------------
IF OBJECT_ID('dbo.estudios', 'U') IS NULL
BEGIN
  CREATE TABLE dbo.estudios (
    id_prof INT         NOT NULL,
    cc_per  BIGINT      NOT NULL,
    fecha   DATE        NULL,
    univer  VARCHAR(50) NULL,
    CONSTRAINT PK_estudios           PRIMARY KEY (id_prof, cc_per),
    CONSTRAINT FK_estudios_profesion FOREIGN KEY (id_prof) REFERENCES dbo.profesion(id),
    CONSTRAINT FK_estudios_persona   FOREIGN KEY (cc_per)  REFERENCES dbo.persona(cc)
  );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'estudio_persona_fk' AND object_id = OBJECT_ID('dbo.estudios'))
BEGIN
  CREATE INDEX estudio_persona_fk ON dbo.estudios(cc_per);
END
GO

-- telefono ---------------------------------------------------
IF OBJECT_ID('dbo.telefono', 'U') IS NULL
BEGIN
  CREATE TABLE dbo.telefono (
    num    VARCHAR(15) NOT NULL,
    oper   VARCHAR(45) NULL,
    duenio BIGINT      NULL,
    CONSTRAINT PK_telefono         PRIMARY KEY (num),
    CONSTRAINT FK_telefono_persona FOREIGN KEY (duenio) REFERENCES dbo.persona(cc)
  );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'telefono_persona_fk' AND object_id = OBJECT_ID('dbo.telefono'))
BEGIN
  CREATE INDEX telefono_persona_fk ON dbo.telefono(duenio);
END
GO
