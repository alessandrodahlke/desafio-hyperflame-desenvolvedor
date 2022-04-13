using AutoMapper;
using BackgroundServices.Application.ViewModels;
using BackgroundServices.Domain.Collections;
using BackgroundServices.Domain.Interfaces;
using BackgroundServices.Services.Interfaces;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IArquivoRepository _repository;
        private readonly IMapper _map;

        public ArquivoService(IArquivoRepository repository,
                           IMapper map)
        {
            _repository = repository;
            _map = map;
        }

        public async Task<ArquivoViewModel> GerarArquivo(ArquivoViewModel arquivoProcessado)
        {
            var arquivoCollection = _map.Map<ArquivoCollection>(arquivoProcessado);

            await _repository.InsertOneAsync(arquivoCollection);

            return _map.Map<ArquivoViewModel>(arquivoCollection);
        }

        public async Task<ArquivoViewModel> ObterArquivo(string id)
        {
            var lote = await _repository.FindByIdAsync(id);

            return _map.Map<ArquivoViewModel>(lote);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
