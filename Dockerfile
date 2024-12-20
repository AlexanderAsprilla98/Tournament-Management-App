# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Install the Entity Framework Core tools
RUN dotnet tool install --global dotnet-ef
# Copy the project files and restore dependencies
COPY . .

RUN dotnet restore
RUN dotnet publish Torneo.App.Frontend/Torneo.App.Frontend.csproj -c Release -o out

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Torneo.App.Frontend.dll"]
