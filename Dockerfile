FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy solution and restore
COPY ["Torneo.App/", "./"]
RUN dotnet restore "Torneo.App.sln"
RUN dotnet build "Torneo.App.sln" -c Release -o /app/build

# Install EF Tools and create migrations
RUN dotnet tool install --global dotnet-ef --version 6.0.8
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN cd Torneo.App.Persistencia && dotnet ef migrations add InitialCreate

# Publish
RUN dotnet publish "Torneo.App.sln" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Install SQL Server tools and .NET SDK
RUN apt-get update && apt-get install -y curl gnupg2 \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev \
    && apt-get install -y wget \
    && wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y dotnet-sdk-6.0

WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

ENV PATH="${PATH}:/root/.dotnet/tools"
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENV PORT=80
EXPOSE 80

ENTRYPOINT ["./entrypoint.sh"]