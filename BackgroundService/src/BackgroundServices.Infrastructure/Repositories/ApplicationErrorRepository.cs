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
    public class ApplicationErrorRepository : IApplicationErrorRepository
    {
        readonly IMongoCollection<ApplicationErrorCollection> _repository;

        public ApplicationErrorRepository(MongoContext context)
        {
            _repository = context.Erros;
        }

        public async Task InsertOneAsync(ApplicationErrorCollection collection)
        {
            await _repository.InsertOneAsync(collection);
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
