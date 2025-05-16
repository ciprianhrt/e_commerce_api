# STAGE 1: Build & Test
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything and restore
COPY . . 
RUN dotnet restore "ECommerceApi.sln"

# Build the solution
RUN dotnet build "ECommerceApi.sln" --configuration Release --no-restore

# Run unit tests
RUN dotnet test "ECommerceApiTests/ECommerceApiTests.csproj" --no-restore --verbosity normal

# Publish the API
RUN dotnet publish "ECommerceApi/ECommerceApi.csproj" -c Release -o /out --no-restore

# STAGE 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Set environment variable to listen on 0.0.0.0:5016
ENV ASPNETCORE_URLS=http://0.0.0.0:5016

# Expose port 5016
EXPOSE 5016

COPY --from=build /out ./
ENTRYPOINT ["dotnet", "ECommerceApi.dll"]
