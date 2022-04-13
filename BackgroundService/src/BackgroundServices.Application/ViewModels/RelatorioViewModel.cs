﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Application.ViewModels
{
    public class RelatorioViewModel
    {
        public string Nome { get; set; }
        public int QuantidadeClientes { get; set; }
        public int QuantidadeVendedores { get; set; }
        public long VendaId { get; set; }
        public string Vendedor { get; set; }
    }
}
