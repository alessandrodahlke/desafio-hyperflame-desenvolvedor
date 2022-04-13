using BackgroundServices.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Domain.Interfaces
{
    public interface IRelatorioRepository : IDisposable
    {
        Task<RelatorioCollection> InsertOneAsync(RelatorioCollection collection);
        Task<RelatorioCollection> FindByIdAsync(string id);
    }
}
