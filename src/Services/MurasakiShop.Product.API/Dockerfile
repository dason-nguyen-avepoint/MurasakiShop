# src/Services/MurasakiShop.Product.API/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Services/MurasakiShop.Product.API/MurasakiShop.Product.API.csproj", "src/Services/MurasakiShop.Product.API/"]
RUN dotnet restore "src/Services/MurasakiShop.Product.API/MurasakiShop.Product.API.csproj"
COPY . .
WORKDIR "/src/src/Services/MurasakiShop.Product.API"
RUN dotnet build "MurasakiShop.Product.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MurasakiShop.Product.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MurasakiShop.Product.API.dll"]