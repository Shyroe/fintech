# FinTech Application

## Desenvolvedor
- **Nome**: Leonardo Camargo
- **Email**: lhc.developerweb@gmail.com
- **LinkedIn**: [LinkedIn](https://www.linkedin.com/in/leonardo-camargo/)
- **Github**: [github.com/Shyroe](https://github.com/Shyroe)

## Descrição do Projeto
Projeto desenvolvido em ASP.NET Core MVC com integração ao SQL Server para realizar gerenciamento das notas fiscais e saber os principais indicadores.
Por meio de uma plataforma com Listagem paginada das notas fiscais e um dashboard para visualização de indicadores e Gráfico de evolução da inadimplência ou da receita recebida mês a mês.

## Tecnologias Utilizadas
- **ASP.NET Core** 8.0
- **Entity Framework Core** 8.0
- **Bootstrap** 5.0
- **ChartJS** 4.4
- **SQL Server**
- **Docker**
- **C#**

## Setup do Projeto

### Pré-requisitos
- [.NET SDK](https://dotnet.microsoft.com/download/dotnet) 8.0
- [Docker](https://www.docker.com/)
- SQL Server ou Docker com SQL Server

### Passos de Instalação

1. **Clonar o Repositório**
   ```bash
   git clone https://github.com/Shyroe/fintech.git

2. **Configurar o Banco de Dados**
	Edite o arquivo appsettings.json com a string de conexão correta:
	**Docker**: Veja informações no arquivo docker-compose.yml
	Exemplo de string de conexão:
	"ConnectionStrings": {
   	   "DefaultConnection": "Server=db;Database=FintechDocker;User=sa;Password=#Docker12300#;TrustServerCertificate=True"
    }
			
	**Sql Server Local**:
    Exemplo de string de conexão:
	"ConnectionStrings": {
   		   "DefaultConnection": "Server=localhost;Database=FinTech;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    }

3. **Aplicar Migrations**
	Instalar ferramenta: dotnet tool install --global dotnet-ef
	Selecione o projeto: FinTech.Data
	
	Rode os seguintes comandos:
	dotnet ef migrations add InitialCreate -Context MeuDbContext
	dotnet ef database update -Context MeuDbContext

4. **Executar a Aplicação**
	Projeto Local:
	Adicione como projeto de Inicialização: FinTech.App
    Rode o comando: dotnet run
		
	Docker Compose: 
	Instale o Docker Desktop se ainda não tiver
    Adicione como projeto de Inicialização: Docker-Compose
	
    Rode os comandos:
	docker-compose up --build (cria os containers)
	docker-compose up -d (roda os containers)

5. **Observações**
   Os códigos sql das tabelas do sistema (Migrations) estão dentro da pasta **sql**

   No Arquivo: Seeds/DataSeeder.cs, tem uma lógica para adicionar dados falsos no banco de dados para testar os filtros