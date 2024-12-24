#!/bin/bash
# Check running containers
docker ps

# If needed, start SQL Server container
docker-compose up -d sql-server

echo "Waiting for SQL Server to be ready..."
for i in {1..30}; do
    if /opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P "${MSSQL_SA_PASSWORD}" -Q "SELECT 1" &>/dev/null; then
        echo "SQL Server is ready"
        break
    fi
    echo "Waiting for SQL Server ($i/30)..."
    sleep 1
done

/opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P "${MSSQL_SA_PASSWORD}" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Torneo') CREATE DATABASE Torneo;"

cd /app/Torneo.App/Torneo.App.Persistencia
echo "Applying migrations..."
# Create a new migration
dotnet ef migrations add InitialCreate --context Torneo.App.Persistencia.DataContext
dotnet ef database update

cd /app/Torneo.App/Torneo.App.Frontend
dotnet ef database update

cd /app
echo "Starting application..."
dotnet Torneo.App.Frontend.dll --urls "http://0.0.0.0:80"