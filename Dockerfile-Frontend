# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the project files and restore dependencies
# Copy the remaining project files and build the application
COPY Torneo.App/ .

RUN dotnet restore

RUN dotnet publish Torneo.App.Frontend/Torneo.App.Frontend.csproj -c Release -o out

# Use a smaller runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build-env
COPY --from=build-env /app/out .

# Expose the port used by the ASP.NET Core application
EXPOSE 80

# Start the ASP.NET Core application when the container starts
ENTRYPOINT ["dotnet", "Torneo.App.Frontend.dll"]
