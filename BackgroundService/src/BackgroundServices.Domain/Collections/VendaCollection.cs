using MongoDB.Bson;
using System.Collections.Generic;

namespace BackgroundServices.Domain.Collections
{
    public class VendaCollection
    {
        public VendaCollection()
        {
            Itens = new List<ItemCollection>();
        }

        public string Id { get; set; }
        public long SaleId { get; set; }
        public string SalesManName { get; set; }
        public List<ItemCollection> Itens { get; set; }
    }
}