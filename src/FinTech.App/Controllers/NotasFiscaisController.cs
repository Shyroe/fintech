using AutoMapper;
using FinTech.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using FinTech.Business.Services;

namespace FinTech.App.Controllers
{
    public class NotasFiscaisController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INotaFiscalRepository _repository;
        private readonly INotaFiscalService _service;
        public NotasFiscaisController(
            INotificador notificador,
            INotaFiscalRepository repository,
            INotaFiscalService service,
            IMapper mapper
            ) : base(notificador)
        {
            _mapper = mapper;
            _repository = repository;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNotasFiscaisList(string status, string mesEmissao, string mesCobranca, string mesPagamento, string draw, string start, string length)
        {
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var statusId = string.IsNullOrEmpty(status) ? null : (int?)Convert.ToInt32(status);

            var query = await _service.GetNotasFiscaisWithStatusAsync(status, mesEmissao, mesCobranca, mesPagamento, skip, pageSize);

            var totalRecords = query.Count();
            var notasFiscais = query
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            var data = notasFiscais.Select(x => new
            {
                id = x.Id,
                nomePagador = x.NomePagador,
                documentoNotaFiscal = x.DocumentoNotaFiscal,
                status = x.Status?.Descricao ?? "",
                dataEmissao = x.DataEmissao.ToString("dd/MM/yyyy"),
                valor = x.Valor.ToString("C2")
            });

            return Json(new { draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data });
        }

    }
}
