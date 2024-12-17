# Bike Rental Application

## Sumário

- [Visão Geral](#visão-geral)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Requisitos de Instalação](#requisitos-de-instalação)
- [Configuração da Aplicação](#configuração-da-aplicação)
- [Rodando a Aplicação Localmente](#rodando-a-aplicação-localmente)
- [Acessando a Aplicação no Docker](#acessando-a-aplicação-no-docker)
- [Conexão com Amazon S3](#conexão-com-amazon-s3)
- [Conexão com RabbitMQ](#conexão-com-rabbitmq)


## Visão Geral

A **Bike Rental Application** é uma plataforma para gerenciar o aluguel de motos. A aplicação permite que entregadores aluguem motos, enviem fotos de suas CNHs, acompanhem o status da locação, atualizem a data de devolução e consultem o valor total da locação. A arquitetura é modular, utilizando tecnologias modernas como Docker, PostgreSQL, RabbitMQ, Amazon S3 e .NET 8.

## Tecnologias Utilizadas

- **Backend**:
  - **.NET 8**: Plataforma para o desenvolvimento backend, oferecendo suporte robusto para APIs RESTful.
  - **PostgreSQL**: Banco de dados relacional para armazenar dados de locações, entregadores, motos e outros metadados.
  - **RabbitMQ**: Servidor de mensagens para comunicação entre serviços, utilizado para publicação e consumo de eventos.
  - **Amazon S3**: Serviço de armazenamento para fotos de CNHs, garantindo escalabilidade e segurança.
  - **Docker**: Para criar contêineres da aplicação, facilitando o deploy e a execução em diferentes ambientes.
  - **Swagger/OpenAPI**: Documentação automática para a API da aplicação.

- **Ferramentas de Desenvolvimento**:
  - **Visual Studio** (ou equivalente para desenvolvimento .NET)
  - **Docker**: Para gerenciar contêineres e ambientes isolados
  - **Postman** ou **Insomnia**: Para testes da API

## Requisitos de Instalação

1. **Ambiente de Desenvolvimento**:
   - **Visual Studio 2022** ou qualquer editor de código compatível com .NET 8
   - **Docker** instalado na máquina (Windows, macOS ou Linux)
   - **Docker Compose** instalado e configurado para suportar Dockerfiles

2. **Dependências**:
   - **AWS Access Key**: Usado para acessar o Amazon S3
   - **Secret Key**: Para autenticação no Amazon S3
   - **Bucket Name** e **Region**: Detalhes necessários para se conectar ao Amazon S3

3. **Credenciais do RabbitMQ**:
   - **Host**: localhost
   - **Porta**: 5672
   - **Usuário**: guest
   - **Senha**: guest

## Configuração da Aplicação

1. **Configurações de Ambiente**:
      - Clone o repositório:
        ```
        git clone https://github.com/NivioP/BikeRentalApp
        ```
     
   - Crie um arquivo `.env` na raiz do projeto e adicione as seguintes variáveis:
        ```
        AWS_AccessKeyId=<Your AWS Access Key>
        AWS_SecretAccessKey=<Your AWS Secret Key>
        AWS_BucketName=<Your S3 Bucket Name>
        AWS_Region=<Your AWS Region>
        ASPNETCORE_ENVIRONMENT=Development
        ```

2. **Configurações do PostgreSQL**:
   - Adicione a string de conexão no arquivo `appsettings.json`:
     ```json
     "ConnectionStrings": {
         "PostgreSQL": "Host=localhost;Port=5432;Database=BikeRentalDB;Username=postgres;Password=yourpassword"
     }
     ```

3. **Configurações do RabbitMQ**:
   - No arquivo `appsettings.json`, adicione as configurações de conexão com o RabbitMQ:
     ```json
     "RabbitMq": {
         "HostName": "localhost",
         "Port": 5672,
         "UserName": "guest",
         "Password": "guest"
     }
     ```

4. **Configurações do Amazon S3**:
   - No arquivo `appsettings.json`, defina as configurações do S3:
     ```json
     "AWS": {
         "AccessKeyId": "<Your AWS Access Key>",
         "SecretAccessKey": "<Your AWS Secret Key>",
         "Region": "<Your AWS Region>",
         "BucketName": "<Your S3 Bucket Name>"
     }
     ```

## Rodando a Aplicação Localmente

1. **Passo 1: Instalação das Dependências**:
   - Certifique-se de ter o Docker e Docker Compose instalados.
   - Instale todas as dependências da aplicação usando o NuGet. Abra o prompt do Visual Studio ou o terminal e navegue até o diretório do projeto.
   - Execute o comando:
     ```bash
     dotnet restore
     ```

2. **Passo 2: Configuração do banco de dados**:
   - Execute as migrações do banco de dados:
     ```bash
     dotnet ef database update
     ```

3. **Passo 3: Compilar e rodar a aplicação**:
   - No Visual Studio, selecione o perfil `http` ou `https` e pressione `Ctrl+F5` para compilar e executar a aplicação.
   - No terminal, navegue até o diretório do projeto e execute:
     ```bash
     dotnet run --profile http
     ```

4. **Passo 4: Acessar a API**:
   - Abra um navegador e acesse `http://localhost:5167` ou `https://localhost:7280` para acessar a documentação da API Swagger.

## Acessando a Aplicação no Docker

1. **Passo 1: Build do Dockerfile**:
   - Navegue até o diretório raiz do projeto onde o `Dockerfile` está localizado.
   - Execute o comando para criar o contêiner:
     ```bash
     docker build -t bike-rental-app .
     ```

2. **Passo 2: Executar o contêiner Docker**:
   - Crie e execute o contêiner usando Docker Compose:
     ```bash
     docker-compose up --build
     ```

3. **Passo 3: Acessar a API**:
   - Abra um navegador e acesse `http://localhost:5167` ou `https://localhost:8081` para acessar a documentação da API Swagger.

## Conexão com Amazon S3

Para armazenar as imagens da CNH, a aplicação utiliza o Amazon S3. As configurações necessárias são definidas no arquivo `appsettings.json`. A conexão é estabelecida com base nas credenciais fornecidas (AccessKey, SecretAccessKey, BucketName, Region).

### Verificar o serviço:
- **Verifique o upload e obtenção da URL da imagem CNH**:
  - Utilize a API POST `/entregadores/{id}/cnh` para enviar a foto CNH.
  - A imagem será salva no S3 com o nome único gerado automaticamente.

## Conexão com RabbitMQ

Para comunicação entre serviços, a aplicação utiliza o RabbitMQ. As configurações necessárias são definidas no arquivo `appsettings.json`.

### Verificar o serviço:
- **Enfileirar e consumir mensagens**:
  - Utilize o serviço de enfileiramento de eventos para publicações e consumos de mensagens.
  

