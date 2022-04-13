using MongoDB.Bson;

namespace BackgroundServices.Domain.Collections
{
    public class ItemCollection 
    {
        public string Id { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}