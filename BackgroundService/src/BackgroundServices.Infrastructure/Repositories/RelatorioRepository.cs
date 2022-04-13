using BackgroundServices.Domain.Collections;
using BackgroundServices.Domain.Interfaces;
using BackgroundServices.Infrastructure.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Infrastructure.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        readonly IMongoCollection<RelatorioCollection> _repository;

        public RelatorioRepository(MongoContext context)
        {
            _repository = context.Relatorio;
        }

        public async Task<RelatorioCollection> InsertOneAsync(RelatorioCollection collection)
        {
            await _repository.InsertOneAsync(collection);
            return collection;
        }
        public async Task<RelatorioCollection> FindByIdAsync(string id)
        {
            return await _repository
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
