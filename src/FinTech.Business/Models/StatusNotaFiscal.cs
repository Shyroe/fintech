﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Business.Models
{
    public class StatusNotaFiscal : Entity
    {
        public new int Id { get; set; }
        public string Descricao { get; set; }
    }
}
