using BackgroundServices.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Interfaces
{
    public interface IArquivoService : IDisposable
    {
        Task<ArquivoViewModel> GerarArquivo(ArquivoViewModel arquivosProcessados);
        Task<ArquivoViewModel> ObterArquivo(string id);
    }
}
