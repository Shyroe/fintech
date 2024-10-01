using FinTech.Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinTech.App.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalEmitidas { get; set; }
        public decimal TotalSemCobranca { get; set; }
        public decimal TotalInadimplencia { get; set; }
        public decimal TotalAVencer { get; set; }
        public decimal TotalPagas { get; set; }
        public IEnumerable<InadimplenciaPorMesDTO> InadimplenciaPorMes { get; set; }
        public IEnumerable<ReceitaPorMesDTO> ReceitaPorMes { get; set; }
        //public List<decimal> InadimplenciaPorMes { get; set; }
        //public List<decimal> ReceitaPorMes { get; set; }
    }

}
