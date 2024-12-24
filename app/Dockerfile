FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy solution files and restore
COPY ["Torneo.App/", "./"]
RUN dotnet restore "Torneo.App.sln"
RUN dotnet build "Torneo.App.sln" -c Release -o /app/build

# Install EF Tools
RUN dotnet tool install --global dotnet-ef --version 6.0.8
ENV PATH="${PATH}:/root/.dotnet/tools"

# Publish and clear NuGet cache
RUN dotnet publish "Torneo.App.sln" -c Release -o /app/publish
RUN dotnet nuget locals all --clear

# Final runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime

# Install SQL Server tools
RUN apt-get update && apt-get install -y curl gnupg2 \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish .
COPY Torneo.App Torneo.App
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

# Install EF Tools
RUN dotnet tool install --global dotnet-ef --version 6.0.8
ENV PATH="${PATH}:/root/.dotnet/tools"
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0
ENV PORT=80
EXPOSE 80

ENTRYPOINT ["./entrypoint.sh"]