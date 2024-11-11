using AutoMapper;
using FinTech.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using FinTech.Business.Services;
using FinTech.App.ViewModels;
using FinTech.Business.Models;
using FinTech.Business.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinTech.App.Extensions;

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

        private void PopulateStatusList()
        {
            var statusList = Enum.GetValues(typeof(StatusNotaFiscalEnum))
                .Cast<StatusNotaFiscalEnum>()
                .Select(e => new { Value = (int)e, Text = e.GetDisplayName() })
                .ToList();

            ViewBag.StatusList = new SelectList(statusList, "Value", "Text");
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

        public IActionResult Create()
        {
            //var notaFiscalViewModel = new NotaFiscalViewModel();
            //return View(notaFiscalViewModel);

            PopulateStatusList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NotaFiscalViewModel request)
        {
            if (!ModelState.IsValid)
            {
                PopulateStatusList();
                return View(request);
            }

            var entity = _mapper.Map<NotaFiscal>(request);
            await _service.Adicionar(entity);

            if (!OperacaoValida())
            {
                PopulateStatusList();
                return View(request);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var entity = await _repository.ObterPorId(id);
            if (entity == null) return NotFound();

            var notaFiscalViewModel = _mapper.Map<NotaFiscalViewModel>(entity);
            PopulateStatusList();
            return View(notaFiscalViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, NotaFiscalViewModel request)
        {

            if (id != request.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                PopulateStatusList();
                return View(request);
            }

            var entity = await _repository.ObterPorId(id);
            entity.NomePagador = request.NomePagador;
            entity.NumeroIdentificacao = request.NumeroIdentificacao;
            entity.DataEmissao = request.DataEmissao;
            entity.DataCobranca = request.DataCobranca;
            entity.DataPagamento = request.DataPagamento;
            entity.Valor = request.Valor;
            entity.DocumentoNotaFiscal = request.DocumentoNotaFiscal;
            entity.DocumentoBoletoBancario = request.DocumentoBoletoBancario;
            entity.StatusId = request.StatusId;

            //var entity = _mapper.Map<NotaFiscal>(request);
            await _service.Atualizar(entity);

            if (!OperacaoValida())
            {
                PopulateStatusList();
                return View(request);
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Details(Guid id)
        {
            var entity = await _repository.ObterPorId(id);
            if (entity == null) return NotFound();

            var notaFiscalViewModel = _mapper.Map<NotaFiscalViewModel>(entity);
            PopulateStatusList();
            return View(notaFiscalViewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _repository.ObterPorId(id);

            if (entity == null) return NotFound();
            var model = _mapper.Map<NotaFiscalViewModel>(entity);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        //[HttpDelete]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var entity = await _repository.ObterPorId(id);

            if (entity == null) return NotFound();

            await _service.Remover(id);

            var model = _mapper.Map<NotaFiscalViewModel>(entity);

            if (!OperacaoValida())
            {
                PopulateStatusList();
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
