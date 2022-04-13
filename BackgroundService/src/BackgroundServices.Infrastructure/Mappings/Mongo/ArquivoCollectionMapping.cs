using BackgroundServices.Domain.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public class ArquivoCollectionMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<ArquivoCollection>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.MapField(x => x.Nome);
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
