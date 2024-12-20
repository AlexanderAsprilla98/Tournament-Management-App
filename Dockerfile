# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Install the Entity Framework Core tools
RUN dotnet tool install --global dotnet-ef --version 6.0.0

WORKDIR /Torneo.App/Torneo.App.Persistencia
RUN dotnet add package Microsoft EntityFrameworkCore Tools
RUN dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.8
RUN dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.8
RUN dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.0.0
RUN dotnet add package Microsoft.AspNetCore.Identity.UI --version 6.0.0

WORKDIR /Torneo.App/Torneo.App.Dominio
RUN dotnet ef migrations add InitialCreate
RUN dotnet ef database update

WORKDIR /app
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