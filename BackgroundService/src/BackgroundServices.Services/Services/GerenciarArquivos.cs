using BackgroundServices.Application.Configurations;
using BackgroundServices.Application.Utils;
using BackgroundServices.Application.ViewModels;
using BackgroundServices.Domain.Collections;
using BackgroundServices.Domain.Interfaces;
using BackgroundServices.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Services
{
    public class GerenciarArquivos : IGerenciarArquivos
    {
        private readonly IArquivoService _arquivoService;
        private readonly IApplicationErrorRepository _applicationErrorRepository;
        private readonly RabbitMQConfig _rabbitMQConfig;
        private string _pathIn;
        private string _extension;

        public GerenciarArquivos(IArquivoService arquivoService,
                                 IApplicationErrorRepository applicationErrorRepository,
                                 IOptions<RabbitMQConfig> rabbitMQConfig,
                                 IConfiguration config)
        {
            _arquivoService = arquivoService;
            _applicationErrorRepository = applicationErrorRepository;
            _rabbitMQConfig = rabbitMQConfig.Value;

            _pathIn = config["Path:Entrada"];
            _extension = config["Path:Extensao"];
        }

        public async Task Gerenciar()
        {
            await ProcessarArquivos();
        }


        public async Task ProcessarArquivos()
        {
            List<ArquivoViewModel> arquivosProcessados = new List<ArquivoViewModel>();
            List<string> arquivosErro = new List<string>();

            string[] arquivos = Directory.GetFiles(_pathIn, _extension);

            if (!arquivos.Any()) return;

            foreach (var arquivo in arquivos)
            {
                //ValidarArquivo();

                try
                {
                    var arquivoProcessado = await ProcessarArquivo(arquivo);

                    if (arquivoProcessado is null)
                        CustomException.BadRequest("Erro ao processar arquivo.");

                    //gravar arquivos processados
                    var arquivoGerado = await _arquivoService.GerarArquivo(arquivoProcessado);

                    await PublicarArquivo(arquivoGerado);
                }
                catch (Exception ex)
                {
                    arquivosErro.Add(arquivo);
                    await _applicationErrorRepository.InsertOneAsync(new ApplicationErrorCollection
                    {
                        Sistema = "Processador-Arquivos",
                        DataHora = DateTime.Now,
                        Mensagem = ex.Message,
                        Stacktrace = ex.StackTrace,
                        Arquivo = arquivo
                    });
                }
            }
        }

        public async Task<ArquivoViewModel> ProcessarArquivo(string arquivo)
        {
            ArquivoViewModel arquivoProcessado = new ArquivoViewModel(arquivo.Split('\\').Last());

            try
            {
                var linhas = File.ReadAllLines(arquivo);

                var vendedores = ObterVendedores(linhas);
                if (vendedores.Any())
                    arquivoProcessado.Vendedores.AddRange(vendedores);

                var clientes = ObterClientes(linhas);
                if (clientes.Any())
                    arquivoProcessado.Clientes.AddRange(clientes);

                var vendas = ObterVendas(linhas);
                if (vendas.Any())
                    arquivoProcessado.Vendas.AddRange(vendas);

            }
            catch (Exception ex)
            {
                await _applicationErrorRepository.InsertOneAsync(new ApplicationErrorCollection
                {
                    Sistema = "Processador-Arquivos",
                    DataHora = DateTime.Now,
                    Mensagem = ex.Message,
                    Stacktrace = ex.StackTrace,
                    Arquivo = arquivo
                });
                return null;
            }

            return arquivoProcessado;
        }

        public IEnumerable<VendedorViewModel> ObterVendedores(string[] linhas)
        {
            var vendedores = from c in
                (from linha in linhas
                 let dados = linha.Split(Separadores.SeparadorColunas)
                 select new VendedorViewModel()
                 {
                     TipoRegistro = dados[0],
                     Cpf = dados[1],
                     Name = dados[2],
                     Salary = dados[3],
                 })
                             where c.TipoRegistro == Separadores.TipoRegistroVendedor
                             select c;

            return vendedores;
        }

        public IEnumerable<ClienteViewModel> ObterClientes(string[] linhas)
        {
            var clientes = from c in
                (from linha in linhas
                 let dados = linha.Split(Separadores.SeparadorColunas)
                 select new ClienteViewModel()
                 {
                     TipoRegistro = dados[0],
                     Cnpj = dados[1],
                     Name = dados[2],
                     BusinessArea = dados[3],
                 })
                           where c.TipoRegistro == Separadores.TipoRegistroCliente
                           select c;
            return clientes;
        }


        public IEnumerable<VendaViewModel> ObterVendas(string[] linhas)
        {
            var vendas = from c in
                (from linha in linhas
                 let dados = linha.Split(Separadores.SeparadorColunas)
                 select new VendaViewModel()
                 {
                     TipoRegistro = dados[0],
                     SaleId = Convert.ToInt64(dados[1]),
                     Itens = dados[0] == Separadores.TipoRegistroVendas
                             ? dados[2].Replace('[', ' ').Replace(']', ' ').Trim().Split(',')
                                        .Select(x => new ItemViewModel
                                        {
                                            ItemId = x.Split('-')[0],
                                            Quantity = x.Split('-')[1],
                                            Price = x.Split('-')[2]
                                        }).ToList()
                             : null,
                     SalesManName = dados[3],
                 })
                         where c.TipoRegistro == Separadores.TipoRegistroVendas
                         select c;

            return vendas;
        }

        public async Task PublicarArquivo(ArquivoViewModel arquivoGerado)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.HostName
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.QueueDeclare(
                    queue: _rabbitMQConfig.Queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var bytesMessage = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new EnvioFila { Lote = false, Id = arquivoGerado.Id }));

            channel.BasicPublish(
                exchange: "",
                routingKey: _rabbitMQConfig.Queue,
                basicProperties: properties,
                body: bytesMessage);
        }

        public void Dispose()
        {
            _arquivoService?.Dispose();
            System.GC.SuppressFinalize(this);
        }
    }
}
