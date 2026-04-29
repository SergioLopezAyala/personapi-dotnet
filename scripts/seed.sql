-- =============================================================
-- seed.sql — DML idempotente para persona_db
-- Alineado con personapi-dotnet/Database/dml.sql
-- =============================================================

USE persona_db;
GO

-- profesion --------------------------------------------------
SET IDENTITY_INSERT dbo.profesion ON;

IF NOT EXISTS (SELECT 1 FROM dbo.profesion WHERE id = 1)
  INSERT INTO dbo.profesion (id, nom, des) VALUES (1, 'Ingeniero de Sistemas', 'Diseño y desarrollo de software');

IF NOT EXISTS (SELECT 1 FROM dbo.profesion WHERE id = 2)
  INSERT INTO dbo.profesion (id, nom, des) VALUES (2, 'Médico General', 'Atención primaria en salud');

IF NOT EXISTS (SELECT 1 FROM dbo.profesion WHERE id = 3)
  INSERT INTO dbo.profesion (id, nom, des) VALUES (3, 'Abogado', 'Asesoría jurídica y litigios');

IF NOT EXISTS (SELECT 1 FROM dbo.profesion WHERE id = 4)
  INSERT INTO dbo.profesion (id, nom, des) VALUES (4, 'Arquitecto', 'Diseño de espacios y construcciones');

IF NOT EXISTS (SELECT 1 FROM dbo.profesion WHERE id = 5)
  INSERT INTO dbo.profesion (id, nom, des) VALUES (5, 'Contador Público', 'Gestión contable y tributaria');

SET IDENTITY_INSERT dbo.profesion OFF;
GO

-- persona ----------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.persona WHERE cc = 1001)
  INSERT INTO dbo.persona (cc, nombre, apellido, genero, edad) VALUES (1001, 'Ana',    'López',     'F', 28);

IF NOT EXISTS (SELECT 1 FROM dbo.persona WHERE cc = 1002)
  INSERT INTO dbo.persona (cc, nombre, apellido, genero, edad) VALUES (1002, 'Luis',   'Gómez',     'M', 35);

IF NOT EXISTS (SELECT 1 FROM dbo.persona WHERE cc = 1003)
  INSERT INTO dbo.persona (cc, nombre, apellido, genero, edad) VALUES (1003, 'María',  'Pérez',     'F', 24);

IF NOT EXISTS (SELECT 1 FROM dbo.persona WHERE cc = 1004)
  INSERT INTO dbo.persona (cc, nombre, apellido, genero, edad) VALUES (1004, 'Carlos', 'Rodríguez', 'M', 42);

IF NOT EXISTS (SELECT 1 FROM dbo.persona WHERE cc = 1005)
  INSERT INTO dbo.persona (cc, nombre, apellido, genero, edad) VALUES (1005, 'Sofía',  'Martínez',  'F', 31);
GO

-- estudios ---------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.estudios WHERE id_prof = 1 AND cc_per = 1001)
  INSERT INTO dbo.estudios (id_prof, cc_per, fecha, univer) VALUES (1, 1001, '2018-06-15', 'Universidad Nacional');

IF NOT EXISTS (SELECT 1 FROM dbo.estudios WHERE id_prof = 2 AND cc_per = 1002)
  INSERT INTO dbo.estudios (id_prof, cc_per, fecha, univer) VALUES (2, 1002, '2015-12-01', 'Universidad de los Andes');

IF NOT EXISTS (SELECT 1 FROM dbo.estudios WHERE id_prof = 3 AND cc_per = 1003)
  INSERT INTO dbo.estudios (id_prof, cc_per, fecha, univer) VALUES (3, 1003, '2020-07-20', 'Universidad Externado');

IF NOT EXISTS (SELECT 1 FROM dbo.estudios WHERE id_prof = 4 AND cc_per = 1004)
  INSERT INTO dbo.estudios (id_prof, cc_per, fecha, univer) VALUES (4, 1004, '2010-05-10', 'Universidad Javeriana');

IF NOT EXISTS (SELECT 1 FROM dbo.estudios WHERE id_prof = 5 AND cc_per = 1005)
  INSERT INTO dbo.estudios (id_prof, cc_per, fecha, univer) VALUES (5, 1005, '2017-11-25', 'Universidad del Rosario');
GO

-- telefono ---------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.telefono WHERE num = '3001112233')
  INSERT INTO dbo.telefono (num, oper, duenio) VALUES ('3001112233', 'Claro',    1001);

IF NOT EXISTS (SELECT 1 FROM dbo.telefono WHERE num = '3014445566')
  INSERT INTO dbo.telefono (num, oper, duenio) VALUES ('3014445566', 'Movistar', 1002);

IF NOT EXISTS (SELECT 1 FROM dbo.telefono WHERE num = '3027778899')
  INSERT INTO dbo.telefono (num, oper, duenio) VALUES ('3027778899', 'Tigo',     1003);

IF NOT EXISTS (SELECT 1 FROM dbo.telefono WHERE num = '3030001122')
  INSERT INTO dbo.telefono (num, oper, duenio) VALUES ('3030001122', 'WOM',      1004);

IF NOT EXISTS (SELECT 1 FROM dbo.telefono WHERE num = '3043334455')
  INSERT INTO dbo.telefono (num, oper, duenio) VALUES ('3043334455', 'ETB',      1005);
GO
