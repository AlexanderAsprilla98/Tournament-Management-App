FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

RUN dotnet tool install --global dotnet-ef  --version 6.0.8
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ["Torneo.App/", "./"]
RUN dotnet restore "Torneo.App.sln"
RUN dotnet build "Torneo.App.sln" -c Release -o /app/build
RUN dotnet publish "Torneo.App.sln" -c Release -o /app/publish

# Create wait-for-sql script
RUN echo '#!/bin/bash\n\
echo "Starting database migration..."\n\
dotnet ef migrations add InitialCreate --project Torneo.App.Persistencia\n\
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