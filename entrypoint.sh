#!/bin/bash

# Wait for the database to become available (adjust the command based on your database type)
# For example, for SQL Server, you might use the following:

# Sleep
sleep 10s

# # Install EF Core tools
# dotnet tool install --global dotnet-ef

# Add EF Core tools to the path
export PATH="${PATH}:/root/.dotnet/tools"

# Build the project
dotnet build -c Release -o out Torneo.App.Persistencia/Torneo.App.Persistencia.csproj 

# Create database migrations
dotnet ef --project Torneo.App.Persistencia/Torneo.App.Persistencia.csproj migrations add InitialCreate

# Apply database migrations
dotnet ef --project Torneo.App.Persistencia/Torneo.App.Persistencia.csproj database update

# Apply database migrations
dotnet ef --project Torneo.App.Frontend/Torneo.App.Frontend.csproj database update

# Start the main application
cd out/ && dotnet Torneo.App.Frontend.dll
