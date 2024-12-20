# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Install the Entity Framework Core tools
RUN dotnet tool install --global dotnet-ef --version 6.0.0

WORKDIR /Torneo.App/Torneo.App.Dominio
Run dotnet ef migrations add InitialCreate
Run dotnet ef database update

# Copy the project files and restore dependencies
COPY . .

# Specify the solution file for restore and publish
RUN dotnet restore Torneo.App/Torneo.App.sln
RUN dotnet publish Torneo.App/Torneo.App.sln -c Release -o out

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Torneo.App.Frontend.dll"]