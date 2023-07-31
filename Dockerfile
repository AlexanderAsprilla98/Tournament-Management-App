# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the project files and restore dependencies
# Copy the remaining project files and build the application
COPY Torneo.App/ .

RUN dotnet restore

RUN dotnet publish Torneo.App.Frontend/Torneo.App.Frontend.csproj -c Release -o out

# Set the ASPNETCORE_URLS environment variable to configure the port that Kestrel listens to
ENV ASPNETCORE_URLS=http://*:80

RUN dotnet tool install --global dotnet-ef

# RUN dotnet ef database update --project Torneo.App.Persistencia/Torneo.App.Persistencia.csproj 
#--startup-project Torneo.App.Frontend/Torneo.App.Frontend.csproj

# Use the official ASP.NET Core runtime image as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

COPY --from=build-env /app/out .
# COPY --from=build-env /app/Torneo.App.Persistencia /app/Torneo.App.Persistencia

# Expose the port used by the ASP.NET Core application
EXPOSE 80

# Start the ASP.NET Core application when the container starts
# ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "Torneo.App.Persistencia/Torneo.App.Persistencia.csproj", "--startup-project", "Torneo.App.Frontend/Torneo.App.Frontend.csproj"]
# COPY entrypoint.sh entrypoint.sh
# RUN chmod +x entrypoint.sh
# ENTRYPOINT [ "./entrypoint.sh" ]
ENTRYPOINT ["dotnet", "Torneo.App.Frontend.dll"]