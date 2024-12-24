#!/bin/bash

# Ensure the script is run as root
if [ "$EUID" -ne 0 ]; then
  echo "Please run as root"
  exit 1
fi
DOCKER_COMPOSE_VERSION=$(curl -s https://api.github.com/repos/docker/compose/releases/latest | grep -Po '"tag_name": "\K.*?(?=")')
curl -L "https://github.com/docker/compose/releases/download/${DOCKER_COMPOSE_VERSION}/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

# Check running containers
docker ps
curl -L 'https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)' -o /usr/local/bin/docker-compose
curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose

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