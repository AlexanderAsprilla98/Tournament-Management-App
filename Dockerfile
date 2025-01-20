FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Install tools, set paths, copy code, and run all build steps in one layer
RUN dotnet tool install --global dotnet-ef --version 8

ENV PATH="${PATH}:/root/.dotnet/tools"
ENV DATABASE_CONNECTION_STRING="Data Source=/app/Torneo.db"

COPY ["Torneo.App/", "./"]
RUN dotnet new sln -n Torneo.App \
    && dotnet sln Torneo.App.sln add \
        Torneo.App.Dominio/Torneo.App.Dominio.csproj \
        Torneo.App.Persistencia/Torneo.App.Persistencia.csproj \
        Torneo.App.Consola/Torneo.App.Consola.csproj \
        Torneo.App.Frontend/Torneo.App.Frontend.csproj \
    && dotnet restore Torneo.App.sln \
    && dotnet build Torneo.App.sln -c Release --no-restore \
    && dotnet ef migrations add InitialCreate \
        --project Torneo.App.Persistencia/Torneo.App.Persistencia.csproj \
        --startup-project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
        --context Torneo.App.Persistencia.DataContext \
        --no-build --configuration Release \
    && dotnet ef database update \
        --project Torneo.App.Persistencia/Torneo.App.Persistencia.csproj \
        --startup-project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
        --context Torneo.App.Persistencia.DataContext \
    && dotnet ef migrations add CreateIdentitySchema \
        --project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
        --context Torneo.App.Frontend.Areas.Identity.Data.IdentityDataContext \
        --no-build --configuration Release \
    && dotnet ef database update \
        --project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
        --startup-project Torneo.App.Frontend/Torneo.App.Frontend.csproj \
        --context Torneo.App.Frontend.Areas.Identity.Data.IdentityDataContext \
    && dotnet publish Torneo.App.sln -c Release -o /app/publish --no-restore --no-build \
    && dotnet nuget locals all --clear

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /app/Torneo.db .
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0
ENV PORT=80
EXPOSE 80
ENTRYPOINT ["dotnet", "Torneo.App.Frontend.dll", "--urls", "http://0.0.0.0:80"]
