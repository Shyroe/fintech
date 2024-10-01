using FinTech.Business.DTOs;
using FinTech.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Business.Intefaces
{
    public interface INotaFiscalRepository : IRepository<NotaFiscal>
    {
        Task<IEnumerable<NotaFiscal>> FiltrarNotasFiscais(string status, string mesEmissao, string mesCobranca, string mesPagamento);
        Task<decimal> ObterTotalNotasEmitidas(int ano, int? mes = null, int? trimestre = null);
        Task<decimal> ObterTotalNotasSemCobranca(int ano, int? mes = null, int? trimestre = null);
        Task<decimal> ObterTotalNotasVencidas(int ano, int? mes = null, int? trimestre = null);
        Task<decimal> ObterTotalNotasAVencer(int ano, int? mes = null, int? trimestre = null);
        Task<decimal> ObterTotalNotasPagas(int ano, int? mes = null, int? trimestre = null);
        Task<IEnumerable<InadimplenciaPorMesDTO>> ObterInadimplenciaPorMes(int ano);
        Task<IEnumerable<ReceitaPorMesDTO>> ObterReceitaPorMes(int ano);
    }
}
