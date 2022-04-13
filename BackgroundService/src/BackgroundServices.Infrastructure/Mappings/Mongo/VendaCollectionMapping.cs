using BackgroundServices.Domain.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public class VendaCollectionMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<VendaCollection>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                //cm.MapIdField(x => x.SaleId);
                cm.MapField(x => x.SalesManName);
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
