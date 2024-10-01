using AutoMapper;
using FinTech.Business.Intefaces;
using FinTech.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FinTech.App.Controllers
{
    public class StatusNotaFiscalController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IStatusNotaFiscalRepository _statusNotaFiscalRepository;
        public StatusNotaFiscalController(
            INotificador notificador,
            IMapper mapper,
            IStatusNotaFiscalRepository statusNotaFiscalRepository

            ) : base(notificador)
        {
            _mapper = mapper;
            _statusNotaFiscalRepository = statusNotaFiscalRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StatusNotaFiscalComboBox()
        {
            var result = await _statusNotaFiscalRepository.ObterTodos();

            return Json(result.Select(item => new
            {
                id = item.Id.ToString(),
                text = item.Descricao,
            }));
        }
    }
}
