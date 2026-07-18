# Fintech Invoicing Dashboard

Aplicação administrativa para cadastro, consulta e acompanhamento de notas fiscais, com filtros, paginação, indicadores financeiros e gráficos de evolução mensal.

O projeto foi desenvolvido como desafio técnico e case de portfólio. Ele demonstra a organização de uma aplicação ASP.NET Core MVC em camadas, integração com SQL Server e tradução de requisitos de negócio em fluxos administrativos.

## Screenshots

### Dashboard

![Dashboard com indicadores](https://github.com/user-attachments/assets/f9a69da5-1dbf-46c2-9fc1-8e163fa2ede3)

![Dashboard com gráficos](https://github.com/user-attachments/assets/96579e85-3208-4ac5-829c-5752860496d1)

### Gestão de notas fiscais

![Filtros de notas fiscais](https://github.com/user-attachments/assets/023d68c9-c88f-40c4-9d4e-c93abf82b1ef)

![Tabela de notas fiscais](https://github.com/user-attachments/assets/01eaf10c-5614-4560-a656-3950982fe02a)

## Principais funcionalidades

- dashboard com indicadores financeiros;
- gráficos de receita recebida e inadimplência por período;
- cadastro e consulta de notas fiscais;
- listagem paginada;
- filtros para localizar e acompanhar registros;
- persistência em SQL Server com Entity Framework Core;
- carga de dados fictícios para demonstração local;
- execução local com Docker Compose.

## Arquitetura

A solução está dividida em três projetos:

```text
src/
├── FinTech.App/       # Aplicação ASP.NET Core MVC, Razor Views e composição
├── FinTech.Business/  # Regras de negócio e validações
└── FinTech.Data/      # Persistência, EF Core, contexto e migrations
```

Essa separação mantém apresentação, regras e acesso a dados em responsabilidades distintas, sem atribuir ao projeto padrões arquiteturais que ele não implementa formalmente.

## Stack

- .NET 8 e ASP.NET Core MVC;
- C#;
- Entity Framework Core 8;
- SQL Server 2022;
- FluentValidation;
- AutoMapper;
- Razor Views;
- Bootstrap 5;
- Chart.js;
- DataTables, Select2 e Flatpickr;
- Docker e Docker Compose;
- LibMan para dependências frontend.

## Executar com Docker

### Requisitos

- Docker Desktop ou Docker Engine com Docker Compose.

### Configuração

Crie um arquivo `.env` na raiz do repositório e defina uma senha local forte para o SQL Server:

```dotenv
MSSQL_SA_PASSWORD=<defina-uma-senha-local-forte>
```

O arquivo `.env` é ignorado pelo Git e não deve ser enviado ao repositório.

### Inicialização

```bash
git clone https://github.com/Shyroe/fintech.git
cd fintech
docker compose up --build
```

A aplicação será exposta em:

```text
http://localhost:5000
```

Para encerrar os containers:

```bash
docker compose down
```

Use `docker compose down -v` somente quando também quiser remover o volume local do SQL Server.

## Executar sem Docker

### Requisitos

- .NET SDK 8;
- SQL Server local;
- ferramenta `dotnet-ef`;
- LibMan CLI.

Configure `ConnectionStrings:DefaultConnection` por variável de ambiente ou ASP.NET Core User Secrets. O repositório não mantém senha ou connection string privada em arquivos versionados.

Depois, restaure e execute:

```bash
dotnet restore FinTech.sln
cd src/FinTech.App
libman restore
cd ../..
dotnet ef database update --project src/FinTech.Data --startup-project src/FinTech.App --context MeuDbContext
dotnet run --project src/FinTech.App
```

## Dados de demonstração

A aplicação possui uma rotina de seed em `Seeds/DataSeeder.cs` para popular o banco local com dados fictícios e permitir a avaliação dos filtros, tabelas, indicadores e gráficos.

## Estado do projeto

- case de portfólio e desafio técnico;
- código público para estudo e avaliação técnica;
- sem demonstração hospedada confirmada;
- não apresentado como sistema em produção;
- a solução atual não inclui um projeto de testes automatizados no arquivo `FinTech.sln`.

## Próximas evoluções

- adicionar testes de regras de negócio e integração;
- automatizar build e validações em CI;
- documentar decisões técnicas com maior profundidade;
- revisar acessibilidade e responsividade das telas administrativas;
- disponibilizar uma demonstração pública quando houver infraestrutura adequada.

## Autor

**Leonardo Camargo** — Software Developer | Frontend Engineering

- [GitHub](https://github.com/Shyroe)
- [LinkedIn](https://www.linkedin.com/in/leonardo-camargo/)
