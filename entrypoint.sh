#!/bin/bash

# Wait for SQL Server to be ready
sleep 30s

# Set up environment variables
export PATH="$PATH:/root/.dotnet/tools"

# Install EF Core tools
dotnet tool install --global dotnet-ef --version 6.0.8

# Run migrations for Persistence project

dotnet ef migrations add InitialCreate --project /app/Torneo.App.Persistencia
dotnet ef database update --project /app/Torneo.App.Persistencia

# Run migrations for Frontend project (Identity)

dotnet ef database update --project /app/Torneo.App.Frontend

# Wait for SQL Server
echo "Waiting for SQL Server to be ready..."
/root/.dotnet/tools/dotnet-ef database update --project /app/Torneo.App.Persistencia

# Start the application
echo "Starting application..."
cd /app
dotnet Torneo.App.Frontend.dll
