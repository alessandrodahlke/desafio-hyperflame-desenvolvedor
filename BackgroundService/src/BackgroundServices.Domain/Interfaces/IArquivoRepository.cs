using BackgroundServices.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Domain.Interfaces
{
    public interface IArquivoRepository : IDisposable
    {
        Task<ArquivoCollection> InsertOneAsync(ArquivoCollection collection);
        Task<ArquivoCollection> FindByIdAsync(string id);
    }
}
