FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Install EF Tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ["Torneo.App/", "./"]
RUN dotnet restore "Torneo.App.sln"
RUN dotnet build "Torneo.App.sln" -c Release -o /app/build
RUN dotnet publish "Torneo.App.sln" -c Release -o /app/publish

# Create startup script
RUN echo '#!/bin/bash\n\
dotnet ef migrations add InitialCreate --project Torneo.App.Persistencia\n\
dotnet ef database update --project Torneo.App.Persistencia\n\
dotnet Torneo.App.Frontend.dll' > /app/publish/startup.sh \
&& chmod +x /app/publish/startup.sh

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./startup.sh"]