#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Imagem de base para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 4000
EXPOSE 4001

# Define variáveis de ambiente conforme especificado para HTTP e HTTPS
ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_HTTPS_PORT=4000
ENV ASPNETCORE_HTTP_PORT=4001
# Para SSL, você precisará de um certificado. A configuração de certificados depende do seu ambiente e pode precisar ser montada no contêiner ou gerada dinamicamente.

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
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Plataforma.Isolar.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plataforma.Isolar.dll"]
