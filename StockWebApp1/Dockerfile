#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["StockWebApp1.csproj", "."]
RUN dotnet restore "./StockWebApp1.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "StockWebApp1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockWebApp1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockWebApp1.dll"]