using System.Collections.Generic;

namespace BackgroundServices.Application.ViewModels
{
    public class VendaViewModel 
    {
        public VendaViewModel()
        {
            Itens = new List<ItemViewModel>();
        }
        public string TipoRegistro { get; set; }
        public long SaleId { get; set; }
        public string SalesManName { get; set; }
        public List<ItemViewModel> Itens { get; set; }
    }
}