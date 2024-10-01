using FinTech.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Business.Intefaces
{
    public interface INotaFiscalService : IDisposable
    {
        Task<List<NotaFiscal>> GetNotasFiscaisWithStatusAsync(string status, string mesEmissao, string mesCobranca, string mesPagamento, int skip, int pageSize);
    }
}
