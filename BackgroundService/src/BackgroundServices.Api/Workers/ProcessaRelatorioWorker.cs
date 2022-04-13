using BackgroundServices.Application.Configurations;
using BackgroundServices.Application.ViewModels;
using BackgroundServices.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundServices.Api.Workers
{
    public class ProcessaRelatorioWorker : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQConfig _rabbitMqConfig;

        public ProcessaRelatorioWorker(IOptions<RabbitMQConfig> rabbitMQConfig,
                                       IServiceProvider serviceProvider)
        {
            _rabbitMqConfig = rabbitMQConfig.Value;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqConfig.HostName
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(
                queue: _rabbitMqConfig.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

                var mensagem = JsonSerializer.Deserialize<EnvioFila>(contentString);

                _ = ProcessarRelatorio(mensagem);
            };
            _channel.BasicConsume(_rabbitMqConfig.Queue, true, consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessarRelatorio(EnvioFila envio)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IRelatorioService>();

            await service.Processar(envio);
        }
    }
}