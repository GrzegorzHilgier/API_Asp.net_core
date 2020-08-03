FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /project
COPY ["src/REST_API/REST_API.csproj", "src/REST_API/"]
COPY . .
WORKDIR "/project/src/REST_API"
RUN dotnet build "REST_API.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "REST_API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "REST_API.dll"]