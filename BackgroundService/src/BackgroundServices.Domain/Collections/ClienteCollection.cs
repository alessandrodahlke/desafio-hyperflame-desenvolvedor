using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackgroundServices.Domain.Collections
{
    public class ClienteCollection 
    {
        public string Id { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string BusinessArea { get; set; }
    }
}