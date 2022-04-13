using BackgroundServices.Domain.Collections;
using System;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Interfaces
{
    public interface IApplicationErrorService : IDisposable
    {
        Task GravarLog(ApplicationErrorCollection collection);
    }
}
