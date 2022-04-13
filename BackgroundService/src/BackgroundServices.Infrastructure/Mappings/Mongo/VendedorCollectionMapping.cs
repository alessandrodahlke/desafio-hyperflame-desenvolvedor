using BackgroundServices.Domain.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public class VendedorCollectionMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<VendedorCollection>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.MapField(x => x.Cpf);
                cm.MapField(x => x.Name);
                cm.MapField(x => x.Salary);
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
