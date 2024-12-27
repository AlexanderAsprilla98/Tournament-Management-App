#!/bin/bash

#echo "Creating database if it does not exist..."
#if [ ! -f "./Torneo.db" ]; then
#    sqlite3 Torneo.db "VACUUM;"
#    echo "Database 'Torneo.db' created."
#else
#    echo "Database 'Torneo.db' already exists."
#fi

if [ ! -f "/app/Torneo.db" ]; then
    if [ -d "/app/Torneo.App/Torneo.App.Persistencia" ]; then
        cd /app/Torneo.App/Torneo.App.Persistencia
    else
        echo "Directory /app/Torneo.App/Torneo.App.Persistencia does not exist."
        exit 1
    fi   
    echo "Checking for existing migrations..."
    # Lista las migraciones y verifica si 'InitialCreate' ya existe
    #existing_migrations_Persintencia=$(dotnet ef migrations list --context Torneo.App.Persistencia.DataContext)
    #if echo "$existing_migrations_Persintencia" | grep -q "InitialCreate"; then
    #    echo "Migration 'InitialCreate' already exists, skipping migration creation."
    #else
    #    echo "Applying migrations for Persistencia..."
    #    dotnet ef migrations add InitialCreate --context Torneo.App.Persistencia.DataContext
    #    if [ $? -ne 0 ]; then
    #        echo "Failed to add migrations for Persistencia."
    #        exit 1
    #    fi
    #fi
    echo "Checking if migrations need to be applied for Persistencia..."
    if [ "$(dotnet ef database update --context Torneo.App.Persistencia.DataContext --dry-run)" ]; then
        echo "Applying migrations for Persistencia..."
        dotnet ef database update --context Torneo.App.Persistencia.DataContext
        if [ $? -ne 0 ]; then
        echo "Failed to update database for Persistencia."
        exit 1
    fi 
    else
        echo "Database is already up to date for Persistencia."
    fi
    if [ -d "/app/Torneo.App/Torneo.App.Frontend" ]; then
        cd /app/Torneo.App/Torneo.App.Frontend
    else
        echo "Directory /app/Torneo.App/Torneo.App.Frontend does not exist."
        exit 1
    fi
    #echo "Checking for existing migrations..."
    ## Lista las migraciones y verifica si 'CreateIdentitySchema' ya existe
    #existing_migrations_Fronted=$(dotnet ef migrations list --context Torneo.App.Frontend.Areas.Identity.Data.IdentityDataContext)
    #if echo "$existing_migrations_Fronted" | grep -q "CreateIdentitySchema"; then
    #    echo "Migration 'CreateIdentitySchema' already exists, skipping migration creation."
    #else
    #    echo "Applying migrations for Frontend..."
    #    dotnet ef migrations add CreateIdentitySchema --context Torneo.App.Frontend.Areas.Identity.Data.IdentityDataContext
    #    if [ $? -ne 0 ]; then
    #        echo "Failed to add migrations for Frontend."
    #        exit 1
    #    fi
    #fi
    echo "Checking if migrations need to be applied for Frontend..."
    if [ "$(dotnet ef database update --context Torneo.App.Frontend.Areas.Identity.Data.IdentityDataContext --dry-run)" ]; then
        echo "Applying migrations for Frontend..."
        dotnet ef database update --context Torneo.App.Frontend.Areas.Identity.Data.IdentityDataContext
        if [ $? -ne 0 ]; then
        echo "Failed to update database for Frontend."
        exit 1
    fi
    else
        echo "Database is already up to date for Frontend."
    fi  
          
else
    echo "Database found, skipping migrations."
fi

cd /app
echo "Starting application..."
exec dotnet Torneo.App.Frontend.dll --urls "http://0.0.0.0:80"