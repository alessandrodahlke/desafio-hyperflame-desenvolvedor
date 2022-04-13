using BackgroundServices.Domain.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public class LoteCollectionMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<LoteCollection>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.MapField(x => x.DataProcessamento);
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
