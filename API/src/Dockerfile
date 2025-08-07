FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project files for restore
COPY TaskManagementSystem.Api/TaskManagementSystem.Api.csproj TaskManagementSystem.Api/
COPY TaskManagementSystem.Data/TaskManagementSystem.Data.csproj TaskManagementSystem.Data/
COPY TaskManagementSystem.Domain/TaskManagementSystem.Domain.csproj TaskManagementSystem.Domain/

# Restore dependencies
RUN dotnet restore TaskManagementSystem.Api/TaskManagementSystem.Api.csproj

# Copy the rest of the source code
COPY TaskManagementSystem.Api/ TaskManagementSystem.Api/
COPY TaskManagementSystem.Data/ TaskManagementSystem.Data/
COPY TaskManagementSystem.Domain/ TaskManagementSystem.Domain/

WORKDIR /src/TaskManagementSystem.Api

RUN dotnet publish TaskManagementSystem.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TaskManagementSystem.Api.dll"]