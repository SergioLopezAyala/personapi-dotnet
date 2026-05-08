# Review and findings

This document summarizes the gaps and pending checks against the project requirements. It is the handoff for applying changes.

## Applied fixes

- Added `Microsoft.EntityFrameworkCore.Tools` to the project references.
- Startup now ensures database creation/migration and seeds data via `PersonaDbSeeder` (no dependency on SQL scripts for local runs).
- Connection string now uses `sa`, and `scripts/schema.sql` sets `persona_db` owner to `sa`.
- README updated to reflect EF Core migrations/seed flow and the new connection string.

## Requirements that appear met (verify anyway)

- No authentication/authorization middleware appears in [personapi-dotnet/Program.cs](../personapi-dotnet/Program.cs#L1-L70).
- The app uses HTTP only in [personapi-dotnet/Properties/launchSettings.json](../personapi-dotnet/Properties/launchSettings.json#L8) and does not call `UseHttpsRedirection()` in [personapi-dotnet/Program.cs](../personapi-dotnet/Program.cs#L1-L70).
- Controllers depend on repository interfaces and do not use `DbContext` directly. Example: [personapi-dotnet/Controllers/PersonaController.cs](../personapi-dotnet/Controllers/PersonaController.cs#L1-L66).
- `DbContext` uses `UseSqlServer` pointing to `persona_db`. See [personapi-dotnet/Program.cs](../personapi-dotnet/Program.cs#L1-L70) and [personapi-dotnet/appsettings.json](../personapi-dotnet/appsettings.json#L10-L12).

## Remaining checks

- Validate the schema against the diagram (`estudios` join table and `telefono.duenio` FK) and confirm constraints/types.

## Manual checks pending

- Confirm SQL Server 2022 is installed.
- Confirm SSMS 18 is installed and working.
- Confirm Visual Studio 2022 Community with workloads:
  - Desarrollo ASP.NET y web
  - Almacenamiento y procesamiento de datos
  - Plantillas de proyecto y elementos de .NET Framework
  - Caracteristicas avanzadas de ASP.NET
- In Visual Studio, confirm SQL Server Object Explorer is open and shows objects.
- Test connection to `persona_db` from Server Explorer and from the running app.
