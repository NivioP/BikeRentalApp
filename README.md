# Bike Rental Application

## Sumário

1. [Visão Geral](#visão-geral)
2. [Tecnologias Utilizadas](#tecnologias-utilizadas)
3. [Requisitos de Instalação](#requisitos-de-instalação)
4. [Configuração da Aplicação](#configuração-da-aplicação)
5. [Rodando a Aplicação Localmente](#rodando-a-aplicação-localmente)
6. [Rodando a Aplicação com Docker](#rodando-a-aplicação-com-docker)
7. [Configurações de Serviços Externos](#configurações-de-serviços-externos)
   - [Amazon S3](#amazon-s3)
   - [RabbitMQ](#rabbitmq)
8. [Documentação da API](#documentação-da-api)
9. [Contato](#contato)

---

## Visão Geral

A **Bike Rental Application** é uma solução desenvolvida em **.NET 8** para gerenciar o aluguel de motos. A aplicação oferece funcionalidades como:

- Cadastro e gerenciamento de motos.
- Locacao de motos por entregadores.
- Upload de fotos da CNH.
- Acompanhamento do status do aluguel.
- Atualização da data de devolução e cálculo do valor total da locação.

A aplicação é **modular**, escalável e utiliza tecnologias modernas para garantir desempenho e confiabilidade.

---

## Tecnologias Utilizadas

- **Backend**:
  - [.NET 8](https://dotnet.microsoft.com/) - Framework principal para desenvolvimento da API.
  - [PostgreSQL](https://www.postgresql.org/) - Banco de dados relacional.
  - [RabbitMQ](https://www.rabbitmq.com/) - Sistema de mensageria para comunicação assíncrona.
  - [Amazon S3](https://aws.amazon.com/s3/) - Armazenamento de arquivos e imagens.
  - [Docker](https://www.docker.com/) - Contêineres para implantação consistente.
  - [Swagger/OpenAPI](https://swagger.io/) - Documentação da API.

- **Ferramentas de Desenvolvimento**:
  - Visual Studio ou VS Code.
  - Docker CLI / Docker Desktop.
  - Postman ou Insomnia para testes da API.

---

## Requisitos de Instalação

### Pré-requisitos
1. **Docker e Docker Compose** instalados na máquina.
   - [Instalar Docker](https://docs.docker.com/get-docker/)
   - [Instalar Docker Compose](https://docs.docker.com/compose/install/)
2. **.NET SDK 8.0** - Para executar o projeto localmente.
   - [Download .NET SDK](https://dotnet.microsoft.com/download)
3. **AWS S3 Access Key e Secret Key** (para upload de arquivos).

---

## Configuração da Aplicação

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/NivioP/BikeRentalApp
   cd BikeRentalApp
   ```

2. **Configurações de ambiente**:
   Atualize o `appsettings.json` com as credenciais corretas:

   - **PostgreSQL**:
     ```json
     "ConnectionStrings": {
       "PostgreSQL": "Host=postgres;Port=5432;Database=appdb;Username=postgres;Password=yourpassword"
     }
     ```

   - **RabbitMQ**:
     ```json
     "RabbitMQ": {
       "HostName": "rabbitmq",
       "Port": 5672,
       "UserName": "admin",
       "Password": "admin"
     }
     ```

   - **Amazon S3**:
     ```json
     "AWS": {
       "AccessKey": "your-access-key",
       "SecretKey": "your-secret-key",
       "Region": "your-region",
       "BucketName": "your-bucket-name"
     }
     ```

---

## Rodando a Aplicação Localmente

### 1. Restaurar pacotes NuGet
Execute o comando para restaurar as dependências:
```bash
dotnet restore
```

### 2. Aplicar migrations no banco de dados
Antes de rodar a aplicação, garanta que o banco PostgreSQL tenha as tabelas criadas:
```bash
dotnet ef database update
```

### 3. Executar a aplicação
No terminal ou no Visual Studio, execute:
```bash
dotnet run
```

A aplicação estará disponível em:
- **Swagger UI**: `http://localhost:8080/swagger`

---

## Rodando a Aplicação com Docker

### 1. Criar e rodar os contêineres
No diretório raiz do projeto, execute:
```bash
docker-compose up --build
```

Os serviços rodarão em:
- **API**: `http://localhost:8080` ou `http://localhost:8081`
- **RabbitMQ**: `http://localhost:15672` (usuário: `admin` | senha: `admin`)
- **PostgreSQL**: Porta `5432`
- **MongoDB**: Porta `27017`
- **MinIO (S3)**: `http://localhost:9000` (console em `http://localhost:9001`)

---

## Configurações de Serviços Externos

### Amazon S3
A aplicação utiliza o S3 para armazenar arquivos de imagem (CNH). Certifique-se de que:

- Você possui uma conta na AWS.
- O bucket está criado.
- AccessKey e SecretKey estão configurados no `appsettings.json`.

### RabbitMQ
RabbitMQ é utilizado para a comunicação assíncrona na aplicação.

- Painel de controle: `http://localhost:15672`
- **Usuário**: `admin`
- **Senha**: `admin`

---

## Documentação da API

A documentação da API é gerada automaticamente pelo Swagger.

- **Acesso**:
  - Local: `http://localhost:8080/swagger`
  - Docker: `http://localhost:8081/swagger`

### Endpoints Principais:
1. **Cadastro e gerenciamento de motos**:
   - `POST /api/motos`
   - `GET /api/motos`
   - `GET /api/motos/{id}`
   - `PUT /api/motos/{id}/placa`
   - `DELETE /api/motos/{id}`

2. **Gerenciamento de Entregadores**:
   - `POST /api/entregadores/`
   - `POST /api/entregadores/{id}/cnh`

3. **Gestão de locações**:
   - `POST /api/locacao`
   - `GET /api/locacao/{id}`
   - `PUT /api/locacao/{id}/devolucao`

---
## Contato

Para dúvidas ou sugestões, entre em contato:

- **Nome**: Nivio Polizzi Limongi
- **Email**: limongi.nivio@gmail.com
- **LinkedIn**: [Seu LinkedIn](https://www.linkedin.com/nivio)
