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

echo "SQL Server listo."

wait
