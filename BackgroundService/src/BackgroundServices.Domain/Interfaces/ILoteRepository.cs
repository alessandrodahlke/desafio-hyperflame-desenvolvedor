using BackgroundServices.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Domain.Interfaces
{
    public interface ILoteRepository : IDisposable
    {
        Task<LoteCollection> InsertOneAsync(LoteCollection collection);
        Task<LoteCollection> FindByIdAsync(string id);
    }
}
