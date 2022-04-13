using System.Collections.Generic;

namespace BackgroundServices.Application.ViewModels
{
    public class ArquivoViewModel
    {
        public ArquivoViewModel(string nome)
        {
            Nome = nome;
            Vendedores = new List<VendedorViewModel>();
            Clientes = new List<ClienteViewModel>();
            Vendas = new List<VendaViewModel>();
        }
        public string Id { get; set; }
        public string Nome { get; set; }
        public List<VendedorViewModel> Vendedores { get; set; }
        public List<ClienteViewModel> Clientes { get; set; }
        public List<VendaViewModel> Vendas { get; set; }
    }
}
