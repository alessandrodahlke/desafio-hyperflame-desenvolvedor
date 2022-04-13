using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace BackgroundServices.Domain.Collections
{
    public class LoteCollection
    {
        public string Id { get; set; }
        public DateTime DataProcessamento { get; set; } = DateTime.Now;
        public IEnumerable<ArquivoCollection> ArquivosProcessados { get; set; }
        
    }
}
