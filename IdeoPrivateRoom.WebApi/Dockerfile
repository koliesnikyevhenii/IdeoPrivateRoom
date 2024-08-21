#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Set the environment variable to "Development"
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IdeoPrivateRoom.WebApi/IdeoPrivateRoom.WebApi.csproj", "IdeoPrivateRoom.WebApi/"]
RUN dotnet restore "./IdeoPrivateRoom.WebApi/IdeoPrivateRoom.WebApi.csproj"
COPY . .
WORKDIR "/src/IdeoPrivateRoom.WebApi"
RUN dotnet build "./IdeoPrivateRoom.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IdeoPrivateRoom.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdeoPrivateRoom.WebApi.dll"]