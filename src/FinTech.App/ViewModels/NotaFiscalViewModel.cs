using FinTech.Business.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinTech.App.ViewModels
{
    public class NotaFiscalViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [DisplayName("Nome do Pagador")]
        public string NomePagador { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [DisplayName("Nº de Identificação")]
        public string NumeroIdentificacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Data de Emissão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataEmissao { get; set; } = DateTime.Now;

        [Display(Name = "Data de Cobrança")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataCobranca { get; set; }

        [Display(Name = "Data de Pagamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O {0} deve ser maior que zero")]
        public decimal Valor { get; set; }

        [DisplayName("Documento Nota Fiscal")]
        public string? DocumentoNotaFiscal { get; set; }

        [DisplayName("Documento Boleto Bancário")]
        public string? DocumentoBoletoBancario { get; set; }

        // Chave estrangeira para StatusNotaFiscal
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Status")]
        public int StatusId { get; set; }
        public StatusNotaFiscalViewModel Status { get; set; }
    }
}
