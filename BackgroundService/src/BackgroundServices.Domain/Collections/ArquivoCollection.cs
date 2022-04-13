using MongoDB.Bson;
using System.Collections.Generic;

namespace BackgroundServices.Domain.Collections
{
    public class ArquivoCollection
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<VendedorCollection> Vendedores { get; set; }
        public IEnumerable<ClienteCollection> Clientes { get; set; }
        public IEnumerable<VendaCollection> Vendas { get; set; }
    }
}
