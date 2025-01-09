# Build stage: Use .NET SDK to compile the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["StrateZoneBackEnd/StrateZone_APIs.csproj", "StrateZoneBackEnd/"]
COPY ["StrateZone_Service/StrateZone_Service.csproj", "StrateZone_Service/"]
COPY ["StrateZone_Repository/StrateZone_Repository.csproj", "StrateZone_Repository/"]

# Restore dependencies
RUN dotnet restore "StrateZoneBackEnd/StrateZone_APIs.csproj"

# Copy the rest of the source code
COPY . .

# Set working directory to the main project
WORKDIR "/src/StrateZoneBackEnd"

# Build the application
RUN dotnet build "StrateZone_APIs.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StrateZone_APIs.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: Use ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StrateZone_APIs.dll"]
