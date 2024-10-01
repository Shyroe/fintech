#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FinTech.App/FinTech.App.csproj", "src/FinTech.App/"]
COPY ["src/FinTech.Data/FinTech.Data.csproj", "src/FinTech.Data/"]
COPY ["src/FinTech.Business/FinTech.Business.csproj", "src/FinTech.Business/"]
RUN dotnet restore "./src/FinTech.App/FinTech.App.csproj"
COPY . .
WORKDIR "/src/src/FinTech.App"
RUN dotnet build "./FinTech.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FinTech.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
# Mudar para o usuário root temporariamente para instalar pacotes
USER root

# Instalar bibliotecas adicionais necessárias
RUN apt-get update && apt-get install -y \
    libkrb5-3 \
    libgssapi-krb5-2 \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Retornar ao usuário app
USER app

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinTech.App.dll"]