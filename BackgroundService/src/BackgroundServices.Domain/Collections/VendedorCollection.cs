using MongoDB.Bson;

namespace BackgroundServices.Domain.Collections
{
    public class VendedorCollection 
    {
        public string Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }
}