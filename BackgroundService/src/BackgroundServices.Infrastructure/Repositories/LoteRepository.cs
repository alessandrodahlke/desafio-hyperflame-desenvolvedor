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
    public class LoteRepository : ILoteRepository
    {
        readonly IMongoCollection<LoteCollection> _repository;

        public LoteRepository(MongoContext context)
        {
            _repository = context.Lotes;
        }

        public async Task<LoteCollection> InsertOneAsync(LoteCollection collection)
        {
            await _repository.InsertOneAsync(collection);
            return collection;
        }
        public async Task<LoteCollection> FindByIdAsync(string id)
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
