using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Application.ViewModels
{
    public class LoteViewModel
    {
        public LoteViewModel(IEnumerable<ArquivoViewModel> arquivosProcessados)
        {
            ArquivosProcessados = arquivosProcessados;
        }

        public string Id { get; set; }
        public DateTime DataProcessamento { get; set; } = DateTime.Now;
        public IEnumerable<ArquivoViewModel> ArquivosProcessados { get; set; }
    }
}
