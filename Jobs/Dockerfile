# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho para o build
WORKDIR /build

# Copia os diretórios dos projetos para o container
COPY ./Plataforma/ ./Plataforma/
COPY ./Teste/ ./Teste/
COPY ./Service/ ./Service/
COPY ./Jobs/ ./Jobs/
COPY ./Infra.Ioc/ ./Infra.Ioc/
COPY ./Infra.Data/ ./Infra.Data/
COPY ./Infra.CrossCutting/ ./Infra.CrossCutting/
COPY ./Dominio/ ./Dominio/

# Define o diretório de trabalho para o projeto Plataforma
WORKDIR /build/Jobs

# Restaura as dependências do projeto
RUN dotnet restore

# Publica o projeto especificando o diretório de output
RUN dotnet publish "Jobs.csproj" -c Release -o /app

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Define a variável de ambiente para o ambiente de desenvolvimento
ENV ASPNETCORE_ENVIRONMENT=Development

# Define o diretório de trabalho para a aplicação
WORKDIR /app

# Copia os binários da etapa de build para o diretório atual
COPY --from=build /app .

# Define o ponto de entrada para o contêiner
ENTRYPOINT ["dotnet", "Jobs.dll"]
