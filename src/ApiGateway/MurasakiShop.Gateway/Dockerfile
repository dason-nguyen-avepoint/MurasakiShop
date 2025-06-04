# src/ApiGateway/CeramicsShop.Gateway/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/ApiGateway/MurasakiShop.Gateway/MurasakiShop.Gateway.csproj", "src/ApiGateway/MurasakiShop.Gateway/"]
RUN dotnet restore "src/ApiGateway/MurasakiShop.Gateway/MurasakiShop.Gateway.csproj"
COPY . .
WORKDIR "/src/src/ApiGateway/MurasakiShop.Gateway"
RUN dotnet build "MurasakiShop.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MurasakiShop.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MurasakiShop.Gateway.dll"]