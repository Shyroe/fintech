using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusNotaFiscal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusNotaFiscal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotasFiscais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NomePagador = table.Column<string>(type: "varchar(100)", maxLength: 150, nullable: false),
                    NumeroIdentificacao = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCobranca = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentoNotaFiscal = table.Column<string>(type: "varchar(100)", maxLength: 200, nullable: true),
                    DocumentoBoletoBancario = table.Column<string>(type: "varchar(100)", maxLength: 200, nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasFiscais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotasFiscais_StatusNotaFiscal_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusNotaFiscal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_StatusId",
                table: "NotasFiscais",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotasFiscais");

            migrationBuilder.DropTable(
                name: "StatusNotaFiscal");
        }
    }
}
