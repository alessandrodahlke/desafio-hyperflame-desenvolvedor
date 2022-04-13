using AutoMapper;
using BackgroundServices.Application.ViewModels;
using BackgroundServices.Domain.Collections;

namespace BackgroundServices.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<VendedorCollection, VendedorViewModel>();
            CreateMap<ClienteCollection, ClienteViewModel>();
            CreateMap<VendaCollection, VendaViewModel>();
            CreateMap<ItemCollection, ItemViewModel>();
            CreateMap<ArquivoCollection, ArquivoViewModel>();
            CreateMap<LoteCollection, LoteViewModel>();
            CreateMap<RelatorioCollection, RelatorioViewModel>();
        }
    }
}
