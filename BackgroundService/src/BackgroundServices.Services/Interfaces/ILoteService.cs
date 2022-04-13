using BackgroundServices.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Interfaces
{
    public interface ILoteService : IDisposable
    {
        Task<LoteViewModel> GerarLote(List<ArquivoViewModel> arquivosProcessados);
        Task<LoteViewModel> ObterLote(string id);
    }
}
