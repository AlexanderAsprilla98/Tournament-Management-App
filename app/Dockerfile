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