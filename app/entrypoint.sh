#!/bin/bash

set -e

echo "Waiting for SQL Server to be ready..."

for i in {1..30}; do
    if /opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P "${MSSQL_SA_PASSWORD}" -Q "SELECT 1" &>/dev/null; then
        echo "SQL Server is ready"
        break
    fi
    echo "Waiting for SQL Server ($i/30)..."
    sleep 1
done

if [ $i -eq 30 ]; then
    echo "SQL Server did not become ready in time"
    exit 1
fi

echo "Creating database if it does not exist..."
/opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P "${MSSQL_SA_PASSWORD}" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Torneo') CREATE DATABASE Torneo;"

cd /app/Torneo.App/Torneo.App.Persistencia
echo "Applying migrations for Persistencia..."
dotnet ef migrations add InitialCreate --context Torneo.App.Persistencia.DataContext || true
dotnet ef database update

cd /app/Torneo.App/Torneo.App.Frontend
echo "Applying migrations for Frontend..."
dotnet ef database update

cd /app
echo "Starting application..."
exec dotnet Torneo.App.Frontend.dll --urls "http://0.0.0.0:80"