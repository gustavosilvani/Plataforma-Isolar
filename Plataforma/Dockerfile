# Imagem de build
FROM mcr.microsoft.com/dotnet/sdk:8.0.101 AS build-env
WORKDIR /app

# Copiar solução e projetos .csproj
COPY *.sln ./
COPY Dominio/Dominio.csproj Dominio/
COPY Infra.CrossCutting/Infra.CrossCutting.csproj Infra.CrossCutting/
COPY Infra.Data/Infra.Data.csproj Infra.Data/
COPY Infra.Ioc/Infra.Ioc.csproj Infra.Ioc/
COPY Service/Service.csproj Service/
COPY Teste/Teste.csproj Teste/
COPY Jobs/Jobs.csproj Jobs/

# Restaurar dependências
RUN dotnet restore

# Copiar o restante dos arquivos e construir o projeto
COPY . ./
RUN dotnet publish -c Release -o out

# Imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app
COPY --from=build-env /app/out .
CMD dotnet Plataforma.dll & dotnet Jobs.dll & wait