#!/bin/bash

#echo "Creating database if it does not exist..."
#if [ ! -f "./Torneo.db" ]; then
#    sqlite3 Torneo.db "VACUUM;"
#    echo "Database 'Torneo.db' created."
#else
#    echo "Database 'Torneo.db' already exists."
#fi


cd /app/Torneo.App/Torneo.App.Persistencia
echo "Remove migrations for Persistencia..."
dotnet ef migrations remove --context Torneo.App.Persistencia.DataContext || true
echo "Applying migrations for Persistencia..."
dotnet ef migrations add InitialCreate --context Torneo.App.Persistencia.DataContext || true
dotnet ef database update

cd /app/Torneo.App/Torneo.App.Frontend
echo "Remove migrations for Frontend..."
dotnet ef migrations remove --context Torneo.App.Frontend.DataContext || true
echo "Applying migrations for Frontend..."
dotnet ef migrations add CreateIdentitySchema --context Torneo.App.Frontend.DataContext || true
dotnet ef database update

cd /app
echo "Starting application..."
exec dotnet Torneo.App.Frontend.dll --urls "http://0.0.0.0:80"