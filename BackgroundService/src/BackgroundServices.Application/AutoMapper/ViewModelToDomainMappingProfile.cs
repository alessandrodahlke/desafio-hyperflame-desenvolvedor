using AutoMapper;
using BackgroundServices.Application.ViewModels;
using BackgroundServices.Domain.Collections;

namespace BackgroundServices.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<VendedorViewModel, VendedorCollection>();
            CreateMap<ClienteViewModel, ClienteCollection>();
            CreateMap<VendaViewModel, VendaCollection>();
            CreateMap<ItemViewModel, ItemCollection>();
            CreateMap<ArquivoViewModel, ArquivoCollection>();
            CreateMap<LoteViewModel, LoteCollection>();
            CreateMap<RelatorioViewModel, RelatorioCollection>();
        }
    }
}
