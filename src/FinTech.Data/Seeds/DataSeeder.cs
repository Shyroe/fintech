using FinTech.Business.Models;
using FinTech.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Data.Seeds
{
    public class DataSeeder
    {
        private readonly MeuDbContext _context;

        public DataSeeder(MeuDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!await _context.StatusNotaFiscal.AnyAsync())
            {
                var status = new List<StatusNotaFiscal>
                {
                    new StatusNotaFiscal { Descricao = "Emitida" },
                    new StatusNotaFiscal { Descricao = "Cobrança Realizada" },
                    new StatusNotaFiscal { Descricao = "Pagamento em Atraso" },
                    new StatusNotaFiscal { Descricao = "Pagamento Realizado" }
                };

                _context.StatusNotaFiscal.AddRange(status);
                await _context.SaveChangesAsync();
            }

            // Remover todos os registros existentes
            _context.NotasFiscais.RemoveRange(_context.NotasFiscais);
            await _context.SaveChangesAsync();

            if (await _context.NotasFiscais.AnyAsync())
            {
                return;
            }

            var notasFiscais = new List<NotaFiscal>
            {
                new NotaFiscal { NomePagador = "Cliente A", NumeroIdentificacao = "NF00001", DataEmissao = new DateTime(2023, 3, 15), DataCobranca = new DateTime(2026, 2, 15), DataPagamento = new DateTime(2023, 3, 1), Valor = 500.00m, DocumentoNotaFiscal = "doc_nota_001.pdf", DocumentoBoletoBancario = "boleto_001.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente B", NumeroIdentificacao = "NF00002", DataEmissao = new DateTime(2023, 2, 20), DataCobranca = new DateTime(2028, 2, 20), DataPagamento = null, Valor = 300.00m, DocumentoNotaFiscal = "doc_nota_002.pdf", DocumentoBoletoBancario = "boleto_002.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente C", NumeroIdentificacao = "NF00003", DataEmissao = new DateTime(2023, 1, 25), DataCobranca = null, DataPagamento = new DateTime(2023, 2, 28), Valor = 1500.00m, DocumentoNotaFiscal = "doc_nota_003.pdf", DocumentoBoletoBancario = "boleto_003.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente D", NumeroIdentificacao = "NF00004", DataEmissao = new DateTime(2022, 3, 5), DataCobranca = new DateTime(2022, 4, 5), DataPagamento = new DateTime(2022, 5, 10), Valor = 800.00m, DocumentoNotaFiscal = "doc_nota_004.pdf", DocumentoBoletoBancario = "boleto_004.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente E", NumeroIdentificacao = "NF00005", DataEmissao = new DateTime(2022, 6, 10), DataCobranca = new DateTime(2025, 7, 10), DataPagamento = null, Valor = 600.00m, DocumentoNotaFiscal = "doc_nota_005.pdf", DocumentoBoletoBancario = "boleto_005.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente F", NumeroIdentificacao = "NF00006", DataEmissao = new DateTime(2021, 8, 15), DataCobranca = null, DataPagamento = new DateTime(2021, 9, 20), Valor = 700.00m, DocumentoNotaFiscal = "doc_nota_006.pdf", DocumentoBoletoBancario = "boleto_006.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente G", NumeroIdentificacao = "NF00007", DataEmissao = new DateTime(2021, 10, 5), DataCobranca = new DateTime(2027, 11, 5), DataPagamento = null, Valor = 1200.00m, DocumentoNotaFiscal = "doc_nota_007.pdf", DocumentoBoletoBancario = "boleto_007.pdf", StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente H", NumeroIdentificacao = "NF00008", DataEmissao = new DateTime(2020, 1, 12), DataCobranca = null, DataPagamento = new DateTime(2020, 3, 1), Valor = 400.00m, DocumentoNotaFiscal = "doc_nota_008.pdf", DocumentoBoletoBancario = "boleto_008.pdf", StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente I", NumeroIdentificacao = "NF00009", DataEmissao = new DateTime(2020, 3, 20), DataCobranca = new DateTime(2027, 4, 20), DataPagamento = null, Valor = 550.00m, DocumentoNotaFiscal = "doc_nota_009.pdf", DocumentoBoletoBancario = "boleto_009.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente J", NumeroIdentificacao = "NF00010", DataEmissao = new DateTime(2020, 5, 8), DataCobranca = new DateTime(2030, 6, 8), DataPagamento = null, Valor = 250.00m, DocumentoNotaFiscal = "doc_nota_010.pdf", DocumentoBoletoBancario = "boleto_010.pdf", StatusId = 3, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente K", NumeroIdentificacao = "NF00011", DataEmissao = new DateTime(2023, 7, 1), DataCobranca = new DateTime(2023, 3, 1), DataPagamento = null, Valor = 1000.00m, DocumentoNotaFiscal = "doc_nota_011.pdf", DocumentoBoletoBancario = "boleto_011.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente L", NumeroIdentificacao = "NF00012", DataEmissao = new DateTime(2023, 4, 10), DataCobranca = null, DataPagamento = null, Valor = 750.00m, DocumentoNotaFiscal = "doc_nota_012.pdf", DocumentoBoletoBancario = "boleto_012.pdf", StatusId = 3, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente M", NumeroIdentificacao = "NF00013", DataEmissao = new DateTime(2023, 8, 15), DataCobranca = new DateTime(2026, 4, 15), DataPagamento = new DateTime(2023, 4, 5), Valor = 920.00m, DocumentoNotaFiscal = "doc_nota_013.pdf", DocumentoBoletoBancario = "boleto_013.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente N", NumeroIdentificacao = "NF00014", DataEmissao = new DateTime(2022, 7, 20), DataCobranca = null, DataPagamento = null, Valor = 1300.00m, DocumentoNotaFiscal = "doc_nota_014.pdf", DocumentoBoletoBancario = "boleto_014.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente O", NumeroIdentificacao = "NF00015", DataEmissao = new DateTime(2022, 9, 30), DataCobranca = null, DataPagamento = null, Valor = 300.00m, DocumentoNotaFiscal = "doc_nota_015.pdf", DocumentoBoletoBancario = "boleto_015.pdf", StatusId = 3, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente P", NumeroIdentificacao = "NF00016", DataEmissao = new DateTime(2021, 11, 5), DataCobranca = new DateTime(2021, 12, 5), DataPagamento = new DateTime(2021, 12, 10), Valor = 1100.00m, DocumentoNotaFiscal = "doc_nota_016.pdf", DocumentoBoletoBancario = "boleto_016.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente Q", NumeroIdentificacao = "NF00017", DataEmissao = new DateTime(2021, 12, 10), DataCobranca = new DateTime(2022, 1, 10), DataPagamento = new DateTime(2022, 1, 15), Valor = 200.00m, DocumentoNotaFiscal = "doc_nota_017.pdf", DocumentoBoletoBancario = "boleto_017.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente R", NumeroIdentificacao = "NF00018", DataEmissao = new DateTime(2021, 5, 12), DataCobranca = new DateTime(2021, 6, 12), DataPagamento = null, Valor = 450.00m, DocumentoNotaFiscal = "doc_nota_018.pdf", DocumentoBoletoBancario = "boleto_018.pdf", StatusId = 3, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente S", NumeroIdentificacao = "NF00019", DataEmissao = new DateTime(2020, 7, 22), DataCobranca = new DateTime(2020, 8, 22), DataPagamento = new DateTime(2020, 9, 5), Valor = 350.00m, DocumentoNotaFiscal = "doc_nota_019.pdf", DocumentoBoletoBancario = "boleto_019.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente T", NumeroIdentificacao = "NF00020", DataEmissao = new DateTime(2020, 2, 1), DataCobranca = new DateTime(2020, 3, 1), DataPagamento = null, Valor = 800.00m, DocumentoNotaFiscal = "doc_nota_020.pdf", DocumentoBoletoBancario = "boleto_020.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente U", NumeroIdentificacao = "NF00021", DataEmissao = new DateTime(2020, 4, 15), DataCobranca = new DateTime(2029, 5, 15), DataPagamento = new DateTime(2020, 6, 5), Valor = 950.00m, DocumentoNotaFiscal = "doc_nota_021.pdf", DocumentoBoletoBancario = "boleto_021.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente V", NumeroIdentificacao = "NF00022", DataEmissao = new DateTime(2022, 11, 20), DataCobranca = new DateTime(2028, 12, 20), DataPagamento = null, Valor = 400.00m, DocumentoNotaFiscal = "doc_nota_022.pdf", DocumentoBoletoBancario = "boleto_022.pdf", StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente W", NumeroIdentificacao = "NF00023", DataEmissao = new DateTime(2021, 8, 10), DataCobranca = null, DataPagamento = new DateTime(2021, 9, 15), Valor = 850.00m, DocumentoNotaFiscal = "doc_nota_023.pdf", DocumentoBoletoBancario = "boleto_023.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente X", NumeroIdentificacao = "NF00024", DataEmissao = new DateTime(2023, 1, 12), DataCobranca = null, DataPagamento = null, Valor = 550.00m, DocumentoNotaFiscal = "doc_nota_024.pdf", DocumentoBoletoBancario = "boleto_024.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente Y", NumeroIdentificacao = "NF00025", DataEmissao = new DateTime(2022, 1, 1), DataCobranca = new DateTime(2022, 2, 1), DataPagamento = null, Valor = 620.00m, DocumentoNotaFiscal = "doc_nota_025.pdf", DocumentoBoletoBancario = "boleto_025.pdf", StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente Z", NumeroIdentificacao = "NF00026", DataEmissao = new DateTime(2021, 3, 8), DataCobranca = new DateTime(2021, 4, 8), DataPagamento = new DateTime(2021, 4, 10), Valor = 370.00m, DocumentoNotaFiscal = "doc_nota_026.pdf", DocumentoBoletoBancario = "boleto_026.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AA", NumeroIdentificacao = "NF00027", DataEmissao = new DateTime(2022, 10, 15), DataCobranca = new DateTime(2022, 11, 15), DataPagamento = new DateTime(2022, 11, 20), Valor = 1300.00m, DocumentoNotaFiscal = "doc_nota_027.pdf", DocumentoBoletoBancario = "boleto_027.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AB", NumeroIdentificacao = "NF00028", DataEmissao = new DateTime(2020, 12, 20), DataCobranca = null, DataPagamento = null, Valor = 280.00m, DocumentoNotaFiscal = "doc_nota_028.pdf", DocumentoBoletoBancario = "boleto_028.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AC", NumeroIdentificacao = "NF00029", DataEmissao = new DateTime(2022, 6, 1), DataCobranca = new DateTime(2026, 7, 1), DataPagamento = new DateTime(2019, 8, 1), Valor = 500.00m, DocumentoNotaFiscal = "doc_nota_029.pdf", DocumentoBoletoBancario = "boleto_029.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AD", NumeroIdentificacao = "NF00030", DataEmissao = new DateTime(2022, 10, 12), DataCobranca = new DateTime(2028, 11, 12), DataPagamento = null, Valor = 1000.00m, DocumentoNotaFiscal = "doc_nota_030.pdf", DocumentoBoletoBancario = "boleto_030.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AE", NumeroIdentificacao = "NF00030", DataEmissao = new DateTime(2023, 5, 12), DataCobranca = null, DataPagamento = null, Valor = 1000.00m, DocumentoNotaFiscal = "doc_nota_030.pdf", DocumentoBoletoBancario = "boleto_030.pdf", StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AF", NumeroIdentificacao = "NF00030", DataEmissao = new DateTime(2023, 6, 12), DataCobranca = null, DataPagamento = null, Valor = 1000.00m, DocumentoNotaFiscal = "doc_nota_030.pdf", DocumentoBoletoBancario = "boleto_030.pdf", StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AG", NumeroIdentificacao = "NF00030", DataEmissao = new DateTime(2023, 9, 5), DataCobranca = new DateTime(2024, 5, 12), DataPagamento = null, Valor = 1000.00m, DocumentoNotaFiscal = "doc_nota_030.pdf", DocumentoBoletoBancario = "boleto_030.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AH", NumeroIdentificacao = "NF00030", DataEmissao = new DateTime(2023, 8, 4), DataCobranca = new DateTime(2024, 4, 5), DataPagamento = null, Valor = 1000.00m, DocumentoNotaFiscal = "doc_nota_030.pdf", DocumentoBoletoBancario = "boleto_030.pdf", StatusId = 4, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AI", NumeroIdentificacao = "NF00030", DataEmissao = new DateTime(2023, 11, 25), DataCobranca = new DateTime(2027, 11, 3), DataPagamento = null, Valor = 1000.00m, DocumentoNotaFiscal = "doc_nota_030.pdf", DocumentoBoletoBancario = "boleto_030.pdf", StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NotaFiscal { NomePagador = "Cliente AJ", NumeroIdentificacao = "NF00030", DataEmissao = new DateTime(2023, 12, 3), DataCobranca = new DateTime(2026, 5, 12), DataPagamento = null, Valor = 1000.00m, DocumentoNotaFiscal = "doc_nota_030.pdf", DocumentoBoletoBancario = "boleto_030.pdf", StatusId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            };

            _context.NotasFiscais.AddRange(notasFiscais);
            await _context.SaveChangesAsync();
        }
    }

}
