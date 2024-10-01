using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinTech.App.ViewModels;
using FinTech.Business.Intefaces;
using FinTech.Business.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinTech.App.Controllers
{
    public class HomeController : BaseController
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;
        public HomeController(
            INotificador notificador,
            INotaFiscalRepository notaFiscalRepository
            ) : base(notificador)
        {
            _notaFiscalRepository = notaFiscalRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ano = DateTime.Now.Year - 1;
            var totalEmitidas = await _notaFiscalRepository.ObterTotalNotasEmitidas(ano);
            var totalNotasSemCobranca = await _notaFiscalRepository.ObterTotalNotasSemCobranca(ano);
            var totalNotasVencidas = await _notaFiscalRepository.ObterTotalNotasVencidas(ano);
            var totalNotasAVencer = await _notaFiscalRepository.ObterTotalNotasAVencer(ano);
            var totalNotasPagas = await _notaFiscalRepository.ObterTotalNotasPagas(ano);

            var viewModel = new DashboardViewModel
            {
                TotalEmitidas = totalEmitidas,
                TotalSemCobranca = totalNotasSemCobranca,
                TotalInadimplencia = totalNotasVencidas,
                TotalAVencer = totalNotasAVencer,
                TotalPagas = totalNotasPagas,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> GetIndicadores(int? ano, int? mes, int? trimestre)
        {
            var anoSelecionado = ano ?? DateTime.Now.Year - 1;

            var totalEmitidas = await _notaFiscalRepository.ObterTotalNotasEmitidas(anoSelecionado, mes, trimestre);
            var totalNotasSemCobranca = await _notaFiscalRepository.ObterTotalNotasSemCobranca(anoSelecionado, mes, trimestre);
            var totalNotasVencidas = await _notaFiscalRepository.ObterTotalNotasVencidas(anoSelecionado, mes, trimestre);
            var totalNotasAVencer = await _notaFiscalRepository.ObterTotalNotasAVencer(anoSelecionado, mes, trimestre);
            var totalNotasPagas = await _notaFiscalRepository.ObterTotalNotasPagas(anoSelecionado, mes, trimestre);

            return Json(new
            {
                totalEmitidas,
                totalNotasSemCobranca,
                totalNotasVencidas,
                totalNotasAVencer,
                totalNotasPagas
            });
        }


        public async Task<IActionResult> GetGraficos(int? ano)
        {
            var anoSelecionado = ano ?? DateTime.Now.Year - 1;
            var inadimplenciaPorMes = await _notaFiscalRepository.ObterInadimplenciaPorMes(anoSelecionado);
            var receitaPorMes = await _notaFiscalRepository.ObterReceitaPorMes(anoSelecionado);

            return Json(new
            {
                inadimplenciaPorMes,
                receitaPorMes
            });
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}
