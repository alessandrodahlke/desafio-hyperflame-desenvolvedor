using BackgroundServices.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Interfaces
{
    public interface IRelatorioService
    {
        Task Processar(EnvioFila arquivo);
        Task<RelatorioViewModel> ObterDadosRelatorio(EnvioFila envio);
    }
}
