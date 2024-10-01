using FinTech.Business.DTOs;
using FinTech.Business.Enums;
using FinTech.Business.Intefaces;
using FinTech.Business.Models;
using FinTech.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Data.Repository
{
    public class NotaFiscalRepository : Repository<NotaFiscal>, INotaFiscalRepository
    {
        public NotaFiscalRepository(MeuDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<NotaFiscal>> FiltrarNotasFiscais(string status, string mesEmissao, string mesCobranca, string mesPagamento)
        {
            var statusId = string.IsNullOrEmpty(status) ? null : (int?)Convert.ToInt32(status);

            var query = Db.NotasFiscais.OrderByDescending(x => x.DataEmissao).AsQueryable();

            if (string.IsNullOrEmpty(status) && string.IsNullOrEmpty(mesEmissao) && string.IsNullOrEmpty(mesCobranca) && string.IsNullOrEmpty(mesPagamento)) return await query.ToListAsync();

            IQueryable<NotaFiscal> queryAcumulador = null;

            if (statusId.HasValue)
            {
                queryAcumulador = query.Where(nf => nf.StatusId == statusId);
            }

            if (!string.IsNullOrEmpty(mesEmissao))
            {
                int mesEmissaoInt = int.Parse(mesEmissao);
                var queryEmissao = query.Where(nf => nf.DataEmissao.Month == mesEmissaoInt);
                queryAcumulador = queryAcumulador == null ? queryEmissao : queryAcumulador.Union(queryEmissao);
            }

            if (!string.IsNullOrEmpty(mesCobranca))
            {
                int mesCobrancaInt = int.Parse(mesCobranca);
                var queryCobranca = query.Where(nf => nf.DataCobranca.HasValue && nf.DataCobranca.Value.Month == mesCobrancaInt);
                queryAcumulador = queryAcumulador == null ? queryCobranca : queryAcumulador.Union(queryCobranca);
            }

            if (!string.IsNullOrEmpty(mesPagamento))
            {
                int mesPagamentoInt = int.Parse(mesPagamento);
                var queryPagamento = query.Where(nf => nf.DataPagamento.HasValue && nf.DataPagamento.Value.Month == mesPagamentoInt);
                queryAcumulador = queryAcumulador == null ? queryPagamento : queryAcumulador.Union(queryPagamento);
            }            

            return await queryAcumulador.Distinct().OrderByDescending(x => x.DataEmissao).ToListAsync();
        }
        
        public async Task<decimal> ObterTotalNotasEmitidas(int ano, int? mes = null, int? trimestre = null)
        {
            var query = Db.NotasFiscais
                                .Where(nf => nf.DataEmissao.Year == ano);

            if (mes.HasValue)
            {
                query = query.Where(nf => nf.DataEmissao.Month == mes.Value);
            }

            if (trimestre.HasValue)
            {
                int startMonth = (trimestre.Value - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                query = query.Where(nf => nf.DataEmissao.Month >= startMonth && nf.DataEmissao.Month <= endMonth);
            }

            return await query.SumAsync(nf => nf.Valor);
        }

        public async Task<decimal> ObterTotalNotasSemCobranca(int ano, int? mes = null, int? trimestre = null)
        {
            var query = Db.NotasFiscais
                                .Where(nf => nf.DataEmissao.Year == ano && nf.DataCobranca == null);

            if (mes.HasValue)
            {
                query = query.Where(nf => nf.DataEmissao.Month == mes.Value);
            }

            if (trimestre.HasValue)
            {
                int startMonth = (trimestre.Value - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                query = query.Where(nf => nf.DataEmissao.Month >= startMonth && nf.DataEmissao.Month <= endMonth);
            }

            return await query.SumAsync(nf => nf.Valor);
        }

        public async Task<decimal> ObterTotalNotasVencidas(int ano, int? mes = null, int? trimestre = null)
        {
            var query = Db.NotasFiscais
                                .Where(nf => nf.DataEmissao.Year == ano && nf.DataCobranca < DateTime.Now && nf.DataPagamento == null);

            if (mes.HasValue)
            {
                query = query.Where(nf => nf.DataEmissao.Month == mes.Value);
            }

            if (trimestre.HasValue)
            {
                int startMonth = (trimestre.Value - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                query = query.Where(nf => nf.DataEmissao.Month >= startMonth && nf.DataEmissao.Month <= endMonth);
            }

            return await query.SumAsync(nf => nf.Valor);
        }

        public async Task<decimal> ObterTotalNotasAVencer(int ano, int? mes = null, int? trimestre = null)
        {
            var query = Db.NotasFiscais
                                .Where(nf => nf.DataEmissao.Year == ano && nf.DataCobranca >= DateTime.Now);

            if (mes.HasValue)
            {
                query = query.Where(nf => nf.DataEmissao.Month == mes.Value);
            }

            if (trimestre.HasValue)
            {
                int startMonth = (trimestre.Value - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                query = query.Where(nf => nf.DataEmissao.Month >= startMonth && nf.DataEmissao.Month <= endMonth);
            }

            return await query.SumAsync(nf => nf.Valor);
        }

        public async Task<decimal> ObterTotalNotasPagas(int ano, int? mes = null, int? trimestre = null)
        {
            var query = Db.NotasFiscais
                                .Where(nf => nf.DataEmissao.Year == ano && nf.DataPagamento != null);

            if (mes.HasValue)
            {
                query = query.Where(nf => nf.DataEmissao.Month == mes.Value);
            }

            if (trimestre.HasValue)
            {
                int startMonth = (trimestre.Value - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                query = query.Where(nf => nf.DataEmissao.Month >= startMonth && nf.DataEmissao.Month <= endMonth);
            }

            return await query.SumAsync(nf => nf.Valor);
        }

        public async Task<IEnumerable<InadimplenciaPorMesDTO>> ObterInadimplenciaPorMes(int ano)
        {
            var inadimplenciaPorMes = await Db.NotasFiscais
                .Where(nf => nf.DataEmissao.Year == ano && nf.StatusId == (int)StatusNotaFiscalEnum.PagamentoAtraso)
                .GroupBy(nf => nf.DataEmissao.Month)
                .Select(g => new InadimplenciaPorMesDTO
                {
                    Mes = g.Key.ToString(),
                    ValorInadimplente = g.Sum(nf => nf.Valor)
                }).ToListAsync();

            var meses = Enumerable.Range(1, 12).ToList();

            var resultado = meses.Select(mes => new InadimplenciaPorMesDTO
            {
                Mes = mes.ToString(),
                ValorInadimplente = inadimplenciaPorMes.FirstOrDefault(inad => inad.Mes == mes.ToString())?.ValorInadimplente ?? 0
            });

            return resultado;
        }

        public async Task<IEnumerable<ReceitaPorMesDTO>> ObterReceitaPorMes(int ano)
        {
            var receitaPorMes = await Db.NotasFiscais
                .Where(nf => nf.DataEmissao.Year == ano && nf.DataPagamento != null)
                .GroupBy(nf => nf.DataPagamento.Value.Month)
                .Select(g => new ReceitaPorMesDTO
                {
                    Mes = g.Key.ToString(),
                    ValorReceita = g.Sum(nf => nf.Valor)
                }).ToListAsync();

            var meses = Enumerable.Range(1, 12).ToList();

            var resultado = meses.Select(mes => new ReceitaPorMesDTO
            {
                Mes = mes.ToString(),
                ValorReceita = receitaPorMes.FirstOrDefault(receita => receita.Mes == mes.ToString())?.ValorReceita ?? 0
            });

            return resultado;
        }

    }
}
