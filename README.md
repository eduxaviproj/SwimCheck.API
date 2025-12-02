# SwimCheck.API

RESTful API para gestão de nadadores e inscrições em competições, construída em C#/.NET.

## Tecnologias utilizadas
- C# / .NET
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- Repository Pattern

## Funcionalidades
- CRUD de nadadores e utilizadores
- Autenticação e autorização com JWT
- Documentação Swagger
- Estrutura em camadas com DTOs, serviços e repositórios

## Endpoints principais
### Auth
POST /api/auth/register
POST /api/auth/login

### Nadadores
GET /api/swimmers
GET /api/swimmers/{id}
POST /api/swimmers
PUT /api/swimmers/{id}
DELETE /api/swimmers/{id}

## Como correr o projeto

1. Restaurar dependências: dotnet restore
2. Criar base de dados e aplicar migrations: dotnet ef database update
3. Executar a API: dotnet run
4. Abrir Swagger: http://localhost:5000/swagger


## Possíveis melhorias
- Refinar arquitetura introduzindo uma Service Layer dedicada
- Implementar testes unitários e de integração
- Adicionar validação com FluentValidation
- Logging estruturado com Serilog
- Dockerização para simplificar setup
