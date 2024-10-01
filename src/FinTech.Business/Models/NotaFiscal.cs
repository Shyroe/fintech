using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Business.Models
{
    public class NotaFiscal : Entity    {
        
        public string NomePagador { get; set; }
        public string NumeroIdentificacao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime? DataCobranca { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public string DocumentoNotaFiscal { get; set; }
        public string DocumentoBoletoBancario { get; set; }

        // Chave estrangeira para StatusNotaFiscal
        public int StatusId { get; set; }
        public StatusNotaFiscal Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
