using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Business.Models.Validations
{
    public class NotaFiscalValidation : AbstractValidator<NotaFiscal>
    {

        public NotaFiscalValidation()
        {
            RuleFor(n => n.NomePagador)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");

            RuleFor(n => n.NumeroIdentificacao)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");

            RuleFor(n => n.DataEmissao)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A {PropertyName} não pode ser uma data futura");

            RuleFor(n => n.DataCobranca)
                .GreaterThan(n => n.DataEmissao).When(n => n.DataCobranca.HasValue)
                .WithMessage("A {PropertyName} deve ser posterior à Data de Emissão");

            RuleFor(n => n.DataPagamento)
                .GreaterThan(n => n.DataCobranca).When(n => n.DataPagamento.HasValue && n.DataCobranca.HasValue)
                .WithMessage("A {PropertyName} deve ser posterior à Data de Cobrança");

            RuleFor(n => n.Valor)
                .GreaterThan(0).WithMessage("O {PropertyName} deve ser maior que zero");

            RuleFor(n => n.DocumentoNotaFiscal)
                .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres")
                .When(n => !string.IsNullOrEmpty(n.DocumentoNotaFiscal));

            RuleFor(n => n.DocumentoBoletoBancario)
                .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres")
                .When(n => !string.IsNullOrEmpty(n.DocumentoBoletoBancario));

            RuleFor(n => n.StatusId)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        }
    }
}
