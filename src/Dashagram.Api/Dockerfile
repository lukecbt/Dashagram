# ---------------------------
# STAGE 1: Build environment
# ---------------------------
# Use the full .NET SDK image (includes compilers, restore tools, etc.)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Set working directory inside the container
WORKDIR /src

# Copy only the .csproj files first.
# This allows Docker to cache the restore layer unless project files change.
COPY Dashagram.Domain/Dashagram.Domain.csproj Dashagram.Domain/
COPY Dashagram.Application/Dashagram.Application.csproj Dashagram.Application/
COPY Dashagram.Infrastructure/Dashagram.Infrastructure.csproj Dashagram.Infrastructure/
COPY Dashagram.Api/Dashagram.Api.csproj Dashagram.Api/

# Restore NuGet packages for the API project (and its referenced projects)
RUN dotnet restore Dashagram.Api/Dashagram.Api.csproj

# Copy the rest of the source code now that restore is cached
COPY Dashagram.Domain/ Dashagram.Domain/
COPY Dashagram.Application/ Dashagram.Application/
COPY Dashagram.Infrastructure/ Dashagram.Infrastructure/
COPY Dashagram.Api/ Dashagram.Api/

# Publish the API project into a self-contained folder
# -c Release → use Release configuration
# -o /app/publish → output folder
# /p:UseAppHost=false → prevents generating a platform-specific executable
RUN dotnet publish Dashagram.Api/Dashagram.Api.csproj -c Release -o /app/publish /p:UseAppHost=false


# ---------------------------
# STAGE 2: Runtime environment
# ---------------------------
# Use the lightweight ASP.NET Core runtime image (no SDK)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

# App will run from /app inside the container
WORKDIR /app

# Install curl for the container health check
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Configure ASP.NET Core to listen on port 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Define a health check that hits your /health endpoint
HEALTHCHECK --interval=30s --timeout=5s --start-period=15s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

# Start the application
ENTRYPOINT ["dotnet", "Dashagram.Api.dll"]
