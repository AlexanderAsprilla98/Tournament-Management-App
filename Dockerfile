# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef --version 6.0.0
RUN dotnet tool update global dotnet ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copy solution and restore dependencies
COPY ["Torneo.App/", "./"]
RUN dotnet restore "Torneo.App.sln"

# Build the project
RUN dotnet build "Torneo.App.sln" -c Release -o /app/build

# Publish
RUN dotnet publish "Torneo.App.sln" -c Release -o /app/publish

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]