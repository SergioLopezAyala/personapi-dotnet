#!/usr/bin/env bash
set -euo pipefail

echo "Contenedor listo."
echo "Dotnet: $(dotnet --version)"
echo "Prueba SQL: sqlcmd -S db -U admin -P 'Admin123!' -Q 'SELECT name FROM sys.databases'"
