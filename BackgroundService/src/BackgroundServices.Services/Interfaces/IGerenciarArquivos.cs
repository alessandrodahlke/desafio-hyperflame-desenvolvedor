using System;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Interfaces
{
    public interface IGerenciarArquivos : IDisposable
    {
        Task Gerenciar();
    }
}
