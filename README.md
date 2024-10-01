# FinTech Application
![Dashboard Indicadores](https://github.com/user-attachments/assets/f9a69da5-1dbf-46c2-9fc1-8e163fa2ede3)
![Dashboard Gr�ficos](https://github.com/user-attachments/assets/96579e85-3208-4ac5-829c-5752860496d1)
![Notas Fiscais Filtros](https://github.com/user-attachments/assets/023d68c9-c88f-40c4-9d4e-c93abf82b1ef)
![Notas Fiscais Tabela](https://github.com/user-attachments/assets/01eaf10c-5614-4560-a656-3950982fe02a)

## Desenvolvedor
- **Nome**: Leonardo Camargo
- **Email**: lhc.developerweb@gmail.com
- **LinkedIn**: [LinkedIn](https://www.linkedin.com/in/leonardo-camargo/)
- **Github**: [github.com/Shyroe](https://github.com/Shyroe)

## Descri��o do Projeto
Projeto desenvolvido em ASP.NET Core MVC com integra��o ao SQL Server para realizar gerenciamento das notas fiscais e saber os principais indicadores.
Por meio de uma plataforma com Listagem paginada das notas fiscais e um dashboard para visualiza��o de indicadores e Gr�fico de evolu��o da inadimpl�ncia ou da receita recebida m�s a m�s.

## Tecnologias Utilizadas
- **ASP.NET Core** 8.0
- **Entity Framework Core** 8.0
- **Bootstrap** 5.0
- **ChartJS** 4.4
- **SQL Server** 2022
- **Docker**
- **C#**

## Setup do Projeto

### Pr�-requisitos
- [.NET SDK](https://dotnet.microsoft.com/download/dotnet) 8.0
- [Docker](https://www.docker.com/)
- SQL Server ou Docker com SQL Server

### Passos de Instala��o

1. **Clonar o Reposit�rio**
   ```bash
   git clone https://github.com/Shyroe/fintech.git

2. **Configurar o Banco de Dados**
	Edite o arquivo appsettings.json com a string de conex�o correta:
	**Docker**: Veja informa��es no arquivo docker-compose.yml
	Exemplo de string de conex�o:
	"ConnectionStrings": {
   	   "DefaultConnection": "Server=db;Database=FintechDocker;User=sa;Password=#Docker12300#;TrustServerCertificate=True"
    }
			
	**Sql Server Local**:
    Exemplo de string de conex�o:
	"ConnectionStrings": {
   		   "DefaultConnection": "Server=localhost;Database=FinTech;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    }

3. **Aplicar Migrations**
	Instalar ferramenta: dotnet tool install --global dotnet-ef
	Selecione o projeto: FinTech.Data
	
	Rode os seguintes comandos:
	dotnet ef migrations add InitialCreate -Context MeuDbContext
	dotnet ef database update -Context MeuDbContext

4. **Executar a Aplica��o**
	Projeto Local:
	Adicione como projeto de Inicializa��o: FinTech.App
    Rode o comando: dotnet run
		
	Docker Compose: 
	Instale o Docker Desktop se ainda n�o tiver
    Adicione como projeto de Inicializa��o: Docker-Compose
	
    Rode os comandos:
	docker-compose up --build (cria os containers)
	docker-compose up -d (roda os containers)

5. **Observa��es**
   Os c�digos sql das tabelas do sistema (Migrations) est�o dentro da pasta **sql**

   No Arquivo: Seeds/DataSeeder.cs, tem uma l�gica para adicionar dados falsos no banco de dados para testar os filtros