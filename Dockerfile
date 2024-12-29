FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
# Install EF Tools
RUN dotnet tool install --global dotnet-ef --version 7.0.8
ENV PATH="${PATH}:/root/.dotnet/tools"

# Create Database
RUN touch /app/Torneo.db

# Copy solution files and restore
COPY ["Torneo.App/", "./"]

RUN dotnet new sln -n Torneo.App

# Add projects to solution
RUN dotnet sln Torneo.App.sln add \
    Torneo.App.Dominio/Torneo.App.Dominio.csproj \
    Torneo.App.Persistencia/Torneo.App.Persistencia.csproj \
    Torneo.App.Consola/Torneo.App.Consola.csproj \
    Torneo.App.Frontend/Torneo.App.Frontend.csproj

RUN dotnet restore "Torneo.App.sln"
RUN dotnet build "Torneo.App.sln" -c Release

# Add migrations and update the database
RUN dotnet ef migrations add InitialCreate \
    --project Torneo.App.Persistencia/Torneo.App.Persistencia.csproj \
    --startup-project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
    --context Torneo.App.Persistencia.DataContext \
    --no-build --configuration Release

RUN dotnet ef database update \
    --project Torneo.App.Persistencia/Torneo.App.Persistencia.csproj \
    --startup-project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
    --context Torneo.App.Persistencia.DataContext

RUN dotnet ef migrations add CreateIdentitySchema \
    --project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
    --context Torneo.App.Frontend.Areas.Identity.Data.IdentityDataContext \
    --no-build --configuration Release

RUN dotnet ef database update \
    --project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
    --context Torneo.App.Frontend.Areas.Identity.Data.IdentityDataContext


# Publish and clear NuGet cache
RUN dotnet publish "Torneo.App.sln" -c Release -o /app/publish
RUN dotnet nuget locals all --clear

# Final runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /app/Torneo.db .

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0
ENV PORT=80
EXPOSE 80

ENTRYPOINT ["dotnet", "Torneo.App.Frontend.dll", "--urls", "http://0.0.0.0:80"]
