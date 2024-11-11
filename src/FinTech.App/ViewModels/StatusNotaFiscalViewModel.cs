using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace FinTech.App.ViewModels
{
    public class StatusNotaFiscalViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }
    }
}
