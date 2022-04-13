using AutoMapper;
using BackgroundServices.Application.ViewModels;
using BackgroundServices.Domain.Collections;
using BackgroundServices.Domain.Interfaces;
using BackgroundServices.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Services
{
    public class LoteService : ILoteService
    {
        private readonly ILoteRepository _repository;
        private readonly IMapper _map;

        public LoteService(ILoteRepository repository,
                           IMapper map)
        {
            _repository = repository;
            _map = map;
        }

        public async Task<LoteViewModel> GerarLote(List<ArquivoViewModel> arquivosProcessados)
        {
            LoteViewModel lote = new LoteViewModel(arquivosProcessados);

            var loteCollection = _map.Map<LoteCollection>(lote);

            await _repository.InsertOneAsync(loteCollection);

            return _map.Map<LoteViewModel>(loteCollection);
        }

        public async Task<LoteViewModel> ObterLote(string id)
        {
            var lote = await _repository.FindByIdAsync(id);

            return _map.Map<LoteViewModel>(lote);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
