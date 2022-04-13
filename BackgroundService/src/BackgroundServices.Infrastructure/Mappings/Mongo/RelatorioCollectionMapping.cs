using BackgroundServices.Domain.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public class RelatorioCollectionMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<RelatorioCollection>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
