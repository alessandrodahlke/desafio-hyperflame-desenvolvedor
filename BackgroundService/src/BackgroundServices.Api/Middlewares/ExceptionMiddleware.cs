using BackgroundServices.Application.Utils;
using BackgroundServices.Domain.Collections;
using BackgroundServices.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BackgroundServices.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private IApplicationErrorService _service;

        public ExceptionMiddleware(RequestDelegate next,
                                   ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                await TratarErro((int)ex.StatusCode, ex, httpContext);
            }
            catch (Exception ex)
            {
                await TratarErro(StatusCodes.Status500InternalServerError, ex, httpContext);
            }
        }

        private async Task TratarErro(int statusCode, Exception ex, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var message = ex switch
            {
                CustomException => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production"
                                            ? ex.Message
                                            : "Internal Server Error from the custom middleware.",
                _ => "Internal Server Error from the custom middleware."
            };

            var erro = new ApplicationErrorCollection()
            {
                Sistema = "Processador-Arquivos",
                DataHora = DateTime.Now,
                Mensagem = message,
                Stacktrace = ex.StackTrace,
                StatusCode = context.Response.StatusCode
            };

            await context.Response.WriteAsync(erro.ToString());

            _logger.LogError($"Erro: {message}");

            await _service.GravarLog(erro);
        }
    }
}
