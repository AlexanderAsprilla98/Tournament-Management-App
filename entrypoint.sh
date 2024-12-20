#!/bin/bash

# Wait for SQL Server to be ready
sleep 30s

# Set up environment variables
export PATH="$PATH:/root/.dotnet/tools"

# Install EF Core tools
dotnet tool install --global dotnet-ef

# Run migrations for Persistence project
cd /app/Torneo.App.Persistencia
dotnet ef database update

# Run migrations for Frontend project (Identity)
cd /app/Torneo.App.Frontend
dotnet ef database update

# Start the application
cd /app
dotnet Torneo.App.Frontend.dll
