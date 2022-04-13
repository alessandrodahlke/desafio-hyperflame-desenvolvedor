using System;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Interfaces
{
    public interface IGerenciarLotesArquivos : IDisposable
    {
        Task Gerenciar();
    }
}
