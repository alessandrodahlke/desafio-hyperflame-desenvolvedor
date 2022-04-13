using BackgroundServices.Domain.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public class ApplicationErrorCollectionMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<ApplicationErrorCollection>(cm =>
            {
                cm.AutoMap();

                cm.MapField(x => x.Arquivo);
                cm.MapField(x => x.DataHora);
                cm.MapField(x => x.Mensagem);
                cm.MapField(x => x.Sistema);
                cm.MapField(x => x.Stacktrace);
                cm.MapField(x => x.StatusCode);
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
