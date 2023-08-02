# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the project files and restore dependencies
# Copy the remaining project files and build the application
COPY Torneo.App/ .

# Set the ASPNETCORE_URLS environment variable to configure the port that Kestrel listens to
ENV ASPNETCORE_URLS=http://*:5000

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet add Torneo.App.Frontend/Torneo.App.Frontend.csproj reference Torneo.App.Dominio/Torneo.App.Dominio.csproj && \
    dotnet add Torneo.App.Frontend/Torneo.App.Frontend.csproj reference Torneo.App.Persistencia/Torneo.App.Persistencia.csproj

RUN dotnet restore Torneo.App.Dominio/Torneo.App.Dominio.csproj
RUN dotnet restore Torneo.App.Persistencia/Torneo.App.Persistencia.csproj

RUN dotnet publish Torneo.App.Frontend/Torneo.App.Frontend.csproj -c Release -o out

# Expose the port used by the ASP.NET Core application
EXPOSE 5000

# Start the ASP.NET Core application when the container starts
COPY entrypoint.sh entrypoint.sh
RUN chmod +x entrypoint.sh
ENTRYPOINT [ "./entrypoint.sh" ]