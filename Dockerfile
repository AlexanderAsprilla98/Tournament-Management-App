FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Install EF Tools
RUN dotnet tool install --global dotnet-ef --version 6.0.8
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ["Torneo.App/", "./"]
RUN dotnet restore "Torneo.App.sln"
RUN dotnet build "Torneo.App.sln" -c Release -o /app/build
RUN dotnet publish "Torneo.App.sln" -c Release -o /app/publish

# Create startup script with logging
RUN echo '#!/bin/bash\n\
echo "Starting database migration..."\n\
dotnet ef migrations add InitialCreate --project Torneo.App.Persistencia || exit 1\n\
echo "Updating database..."\n\
dotnet ef database update --project Torneo.App.Persistencia || exit 1\n\
echo "Starting application..."\n\
dotnet Torneo.App.Frontend.dll' > /app/publish/startup.sh \
&& chmod +x /app/publish/startup.sh

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./startup.sh"]