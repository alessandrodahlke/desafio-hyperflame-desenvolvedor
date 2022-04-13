using BackgroundServices.Domain.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BackgroundServices.Infrastructure.Context
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration config)
        {
            var conn = config["MongoDb:Url"];
            var database = config["MongoDb:DataBase"];

            var mongoClient = new MongoClient(conn);
            _database = mongoClient.GetDatabase(database);
        }

        public IMongoCollection<LoteCollection> Lotes =>
            _database.GetCollection<LoteCollection>("lotes");

        public IMongoCollection<ArquivoCollection> Arquivos =>
            _database.GetCollection<ArquivoCollection>("arquivos");

        public IMongoCollection<ApplicationErrorCollection> Erros =>
            _database.GetCollection<ApplicationErrorCollection>("erros");

        public IMongoCollection<RelatorioCollection> Relatorio =>
            _database.GetCollection<RelatorioCollection>("relatorios");
    }
}
