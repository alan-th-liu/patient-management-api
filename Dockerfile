# Multi-stage Dockerfile for patient-management-api (.NET 9)

# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj first to leverage Docker layer caching for restore
COPY patient-management-api/patient-management-api.csproj patient-management-api/
RUN dotnet restore "patient-management-api/patient-management-api.csproj"

# Copy the rest of the source
COPY . .
WORKDIR /src/patient-management-api

# Publish as self-contained framework-dependent app
RUN dotnet publish -c Release -o /app/out --no-restore

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Listen on 8080 inside the container
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_PRINT_TELEMETRY_MESSAGE=false

# Expose port 8080 (map from host as needed)
EXPOSE 8080

# Copy published output
COPY --from=build /app/out .

# Default entrypoint
ENTRYPOINT ["dotnet", "patient-management-api.dll"]
