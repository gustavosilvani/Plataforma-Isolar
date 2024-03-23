# Imagem de base para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 4001 # Expondo apenas a porta HTTP

# Define variáveis de ambiente para informar a aplicação sobre as portas utilizadas
ENV ASPNETCORE_URLS=http://+:4001

# Imagem de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Plataforma.Isolar/Plataforma.Isolar.csproj", "Plataforma.Isolar/"]
RUN dotnet restore "Plataforma.Isolar/Plataforma.Isolar.csproj"
COPY . .
WORKDIR "/src/Plataforma.Isolar"
RUN dotnet build "Plataforma.Isolar.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Imagem de publish
FROM build AS publish
RUN dotnet publish "Plataforma.Isolar.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plataforma.Isolar.dll"]
