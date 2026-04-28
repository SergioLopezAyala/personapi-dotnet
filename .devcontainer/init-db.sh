#!/usr/bin/env bash
set -euo pipefail

/opt/mssql/bin/sqlservr &

echo "Esperando a SQL Server..."
for i in {1..60}; do
  if /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -Q "SELECT 1" > /dev/null 2>&1; then
    listo="si"
    break
  fi
  sleep 2
done

if [[ "${listo:-no}" != "si" ]]; then
  echo "SQL Server no respondio a tiempo."
  exit 1
fi

echo "Ejecutando scripts de base de datos..."
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -i /scripts/schema.sql
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -i /scripts/seed.sql

wait
