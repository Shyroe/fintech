using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Business.Enums
{
    public enum StatusNotaFiscalEnum
    {
        [Display(Name = "Emitida")]
        Emitida = 1,

        [Display(Name = "Cobrança Realizada")]
        CobrancaRealizada = 2,

        [Display(Name = "Pagamento em Atraso")]
        PagamentoAtraso = 3,

        [Display(Name = "Pagamento Realizado")]
        PagamentoRealizado = 4
    }
}
