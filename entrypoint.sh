#!/bin/bash

# Wait for SQL Server
echo "Waiting for SQL Server to be ready..."
for i in {1..30}; do
    if /opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P "${MSSQL_SA_PASSWORD}" -Q "SELECT 1" &>/dev/null; then
        echo "SQL Server is ready"
        break
    fi
    echo "Waiting for SQL Server ($i/30)..."
    sleep 1
done

# Start the application
echo "Starting application..."
dotnet Torneo.App.Frontend.dll