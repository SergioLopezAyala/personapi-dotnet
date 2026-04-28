# Entorno base para .NET Lab

Este repositorio provee un entorno de desarrollo reutilizable con Dev Containers para .NET 7 y SQL Server 2022, con dependencias fijadas y un proyecto Web API minimo.

## Requisitos previos

- Docker Desktop
- VS Code o Cursor con extension Dev Containers

## Como abrir el entorno

1. Abrir el repositorio en VS Code o Cursor.
2. Ejecutar la accion "Reopen in Container".
3. Esperar a que se construyan los contenedores.

## Que incluye

- .NET SDK 7.0.410 (fijado en [global.json](global.json))
- SQL Server 2022 con scripts de inicializacion automaticos
- Versiones fijas de paquetes en [Directory.Packages.props](Directory.Packages.props)
- Proyecto base Web API en [personapi-dotnet/](personapi-dotnet)

## Verificacion rapida

### .NET

Dentro del contenedor:

```bash
dotnet --version
```

Debe mostrar `7.0.410`.

### API

```bash
cd personapi-dotnet
dotnet run
```

La app debe escuchar en `http://localhost:5000`.

### SQL Server

```bash
sqlcmd -S db -U admin -P "Admin123!" -Q "SELECT name FROM sys.databases"
```

Debes ver `persona_db` en la lista.

## Recursos configurados

- App: `mem_limit: 2g`, `cpus: 1.5`
- SQL Server: `mem_limit: 2g`, `cpus: 1.5`
- Volumenes persistentes para NuGet y datos de SQL

## Scripts de base de datos

- [scripts/schema.sql](scripts/schema.sql) crea `persona_db` y la tabla `Persona`
- [scripts/seed.sql](scripts/seed.sql) inserta datos de ejemplo

Los scripts se ejecutan automaticamente al iniciar el contenedor de SQL Server.

## Notas

- HTTP solamente (sin HTTPS)
- Sin autenticacion
- Usuario SQL: `admin`
- Contrasena SQL: `Admin123!`
# Net-Lab
