# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and restore dependencies
COPY . .
RUN dotnet restore

# Publish (creates a self-contained folder ready to run)
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy published output from build stage
COPY --from=build /app/publish .

# Run the API
# NOTE: Replace with your real dll name (project name)
ENTRYPOINT ["dotnet", "SuperStore.Api.dll"]
