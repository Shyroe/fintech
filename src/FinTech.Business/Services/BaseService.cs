using FinTech.Business.Intefaces;
using FinTech.Business.Models;
using FinTech.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace FinTech.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            // Serve para fazer a propagação do Erorr (Mensagem de error) até a camada de Apresentação
            // Ou seja, este método  _notificador.Handle(), serve para levar esta mensagem de error até o frontend (camada de Apresentação)
            _notificador.Handle(new Notificacao(mensagem));
        }


        // Metodo de Validação, que é usado dentro dos Services para validar os dados

        //ExecutarValidacao() é um método Genérico, é desta forma que se cria um método genérico
        // ExecutarValidacao<TV, TE>(TV validacao, TE entidade) - T (serve para informar que é do tipo genérico.
        // TV / where TV : AbstractValidator<TE> - significa que é uma propriedade genérica que implementa a classe AbstractValidator<TE>
        // TE / where TE : Entity - significa que é uma propriedade genérica que implementa a classe Entity
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {

            // validator é a variavel que armazena o resultado do método validacao.Validate()
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}