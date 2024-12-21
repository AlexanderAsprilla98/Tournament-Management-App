FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ["Torneo.App/", "./"]
RUN dotnet restore "Torneo.App.sln"
RUN dotnet build "Torneo.App.sln" -c Release -o /app/build
RUN dotnet publish "Torneo.App.sln" -c Release -o /app/publish

# Create wait-for-sql script
RUN echo '#!/bin/bash\n\
until /opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P $MSSQL_SA_PASSWORD -Q "SELECT 1" > /dev/null 2>&1; do\n\
  echo "Waiting for SQL Server..."\n\
  sleep 5\n\
done\n\
echo "SQL Server is ready"\n\
\n\
echo "Running migrations..."\n\
cd /app/Torneo.App.Persistencia\n\
dotnet ef database update\n\
\n\
echo "Starting application..."\n\
cd /app\n\
dotnet Torneo.App.Frontend.dll' > /app/publish/start.sh \
&& chmod +x /app/publish/start.sh

FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN apt-get update && apt-get install -y curl gnupg2 \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./start.sh"]