using System;
using System.Text.Json;

namespace BackgroundServices.Domain.Collections
{
    public class ApplicationErrorCollection
    {
        public string Arquivo { get; set; }
        public DateTime DataHora { get; set; }
        public string Sistema { get; set; }
        public string Stacktrace { get; set; }
        public string Mensagem { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
