using BackgroundServices.Domain.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public class ItemCollectionMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<ItemCollection>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.MapField(x => x.Quantity);
                cm.MapField(x => x.Price);
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
