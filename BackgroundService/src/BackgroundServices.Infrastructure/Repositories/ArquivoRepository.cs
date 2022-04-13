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
    public class ArquivoRepository : IArquivoRepository
    {
        readonly IMongoCollection<ArquivoCollection> _repository;

        public ArquivoRepository(MongoContext context)
        {
            _repository = context.Arquivos;
        }

        public async Task<ArquivoCollection> InsertOneAsync(ArquivoCollection collection)
        {
            await _repository.InsertOneAsync(collection);
            return collection;
        }
        public async Task<ArquivoCollection> FindByIdAsync(string id)
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
