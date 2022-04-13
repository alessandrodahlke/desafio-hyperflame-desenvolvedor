using BackgroundServices.Domain.Collections;
using BackgroundServices.Domain.Interfaces;
using BackgroundServices.Services.Interfaces;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Services
{
    public class ApplicationErrorService : IApplicationErrorService
    {
        private readonly IApplicationErrorRepository _repository;
        public ApplicationErrorService(IApplicationErrorRepository repository)
        {
            _repository = repository;
        }

        public async Task GravarLog(ApplicationErrorCollection collection)
        {
            await _repository.InsertOneAsync(collection);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
