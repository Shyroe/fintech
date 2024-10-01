using System.Collections.Generic;
using FinTech.Business.Notificacoes;

namespace FinTech.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}