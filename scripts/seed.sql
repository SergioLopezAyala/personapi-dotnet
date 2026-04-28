USE persona_db;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Persona WHERE Id = 1)
  INSERT INTO dbo.Persona (Nombre, Apellido, Email, FechaNacimiento)
  VALUES ('Ana', 'Lopez', 'ana.lopez@example.com', '1998-05-12');

IF NOT EXISTS (SELECT 1 FROM dbo.Persona WHERE Id = 2)
  INSERT INTO dbo.Persona (Nombre, Apellido, Email, FechaNacimiento)
  VALUES ('Luis', 'Gomez', 'luis.gomez@example.com', '1995-11-03');

IF NOT EXISTS (SELECT 1 FROM dbo.Persona WHERE Id = 3)
  INSERT INTO dbo.Persona (Nombre, Apellido, Email, FechaNacimiento)
  VALUES ('Maria', 'Perez', 'maria.perez@example.com', '2000-01-21');
GO
