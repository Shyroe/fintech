using FinTech.Business.Intefaces;
using FinTech.Business.Models;
using FinTech.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Data.Repository
{
    public class StatusNotaFiscalRepository : Repository<StatusNotaFiscal>, IStatusNotaFiscalRepository
    {
        public StatusNotaFiscalRepository(MeuDbContext db) : base(db)
        {
        }
    }   
}
