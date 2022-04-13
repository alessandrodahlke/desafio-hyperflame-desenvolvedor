using BackgroundServices.Domain.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public class ClienteCollectionMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<ClienteCollection>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.MapField(x => x.Cnpj);
                cm.MapField(x => x.Name);
                cm.MapField(x => x.BusinessArea);
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
