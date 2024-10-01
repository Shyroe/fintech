using FinTech.Business.Intefaces;
using FinTech.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Business.Services
{
    public class NotaFiscalService : BaseService, INotaFiscalService
    {
        private readonly INotaFiscalRepository _repository;
        private readonly IStatusNotaFiscalRepository _statusNfRepository;

        public NotaFiscalService(
            INotificador notificador,
            INotaFiscalRepository repository,
            IStatusNotaFiscalRepository statusNfRepository
            ) : base(notificador)
        {
            _repository = repository;
            _statusNfRepository = statusNfRepository;
        }

        public async Task<List<NotaFiscal>> GetNotasFiscaisWithStatusAsync(string status, string mesEmissao, string mesCobranca, string mesPagamento, int skip, int pageSize)
        {
            // Filtro de status
            //var statusId = string.IsNullOrEmpty(status) ? null : (int?)Convert.ToInt32(status);


            var query = await _repository.FiltrarNotasFiscais(status, mesEmissao, mesCobranca, mesPagamento);
            List<NotaFiscal> notasFiscais = query.ToList();
            
            var statusIds = notasFiscais.Select(nf => nf.StatusId).Distinct();
            var statusList = (await _statusNfRepository
                .Buscar(s => statusIds.Contains(s.Id)))
                .ToList();
            
            var statusMap = statusList.ToDictionary(s => s.Id, s => s);
                        
            foreach (var nota in notasFiscais)
            {
                if (statusMap.ContainsKey(nota.StatusId))
                {
                    nota.Status = statusMap[nota.StatusId];
                }
            }

            return notasFiscais;
        }


        public void Dispose()
        {
            _repository?.Dispose();
            _statusNfRepository?.Dispose();
        }
    }
}
