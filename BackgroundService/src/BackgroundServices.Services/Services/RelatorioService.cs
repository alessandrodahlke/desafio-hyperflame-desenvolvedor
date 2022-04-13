using AutoMapper;
using BackgroundServices.Application.Utils;
using BackgroundServices.Application.ViewModels;
using BackgroundServices.Domain.Collections;
using BackgroundServices.Domain.Interfaces;
using BackgroundServices.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Services.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly ILoteService _loteService;
        private readonly IArquivoService _arquivoService;
        private readonly IRelatorioRepository _repository;
        private readonly IApplicationErrorRepository _applicationErrorRepository;
        private readonly IMapper _map;
        private string _pathOut;

        public RelatorioService(ILoteService loteService,
                                IArquivoService arquivoService,
                                IRelatorioRepository repository,
                                IApplicationErrorRepository applicationErrorRepository,
                                IMapper map,
                                IConfiguration config)
        {
            _loteService = loteService;
            _arquivoService = arquivoService;
            _repository = repository;
            _applicationErrorRepository = applicationErrorRepository;
            _map = map;
            _pathOut = config["Path:Saida"];
        }

        public async Task Processar(EnvioFila envio)
        {
            var dadosRelatorio = await ObterDadosRelatorio(envio);

            if (dadosRelatorio is null) return;

            await GerarRelatorio(dadosRelatorio);

            await GerarArquivo(dadosRelatorio);
        }

        public async Task<RelatorioViewModel> ObterDadosRelatorio(EnvioFila envio)
        {
            if (envio.Lote)
            {
                var lote = await _loteService.ObterLote(envio.Id);

                if (lote is null) CustomException.BadRequest("Lote de Arquivos não encontrado.");

                var vendas = lote.ArquivosProcessados.SelectMany(x => x.Vendas).ToList();

                return new RelatorioViewModel
                {
                    QuantidadeClientes = lote.ArquivosProcessados.Sum(x => x.Clientes.Count()),
                    QuantidadeVendedores = lote.ArquivosProcessados.Sum(x => x.Vendedores.Count()),
                    VendaId = vendas.OrderByDescending(x => x.Itens.Sum(i => decimal.Parse(i.Price)) > 0).FirstOrDefault().SaleId,
                    Vendedor = vendas.OrderBy(x => x.Itens.Sum(i => decimal.Parse(i.Price)) > 0).FirstOrDefault().SalesManName
                };
            }

            var arquivo = await _arquivoService.ObterArquivo(envio.Id);

            if (arquivo is null) CustomException.BadRequest("Arquivo não encontrado.");

            return new RelatorioViewModel
            {
                Nome = arquivo.Nome,
                QuantidadeClientes = arquivo.Clientes.Count(),
                QuantidadeVendedores = arquivo.Vendedores.Count(),
                VendaId = arquivo.Vendas.OrderByDescending(x => x.Itens.Sum(i => decimal.Parse(i.Price)) > 0).FirstOrDefault().SaleId,
                Vendedor = arquivo.Vendas.OrderBy(x => x.Itens.Sum(i => decimal.Parse(i.Price)) > 0).FirstOrDefault().SalesManName
            };

        }
        public async Task GerarRelatorio(RelatorioViewModel viewModel)
        {
            try
            {
                var collection = _map.Map<RelatorioCollection>(viewModel);

                await _repository.InsertOneAsync(collection);
            }
            catch (Exception ex)
            {
                await _applicationErrorRepository.InsertOneAsync(new ApplicationErrorCollection
                {
                    Sistema = "Processador-Relatorio",
                    DataHora = DateTime.Now,
                    Mensagem = ex.Message,
                    Stacktrace = ex.StackTrace
                });
            }
        }

        public async Task GerarArquivo(RelatorioViewModel dados)
        {
            var nome = string.IsNullOrEmpty(dados.Nome) ? $"{DateTime.Now.ToString("dd_mm_yyyy_HH_mm_ss")}" : dados.Nome.Split('.').First();

            string nomeArquivo = $"{_pathOut}/{nome}.done.dat";

            try
            {
                await using StreamWriter file = new(File.Open(nomeArquivo, FileMode.CreateNew), Encoding.GetEncoding("UTF-8"));

                var text = $"{dados.QuantidadeClientes}ç{dados.QuantidadeVendedores}ç{dados.VendaId}ç{dados.Vendedor}";

                await file.WriteLineAsync($"{dados.QuantidadeClientes}ç{dados.QuantidadeVendedores}ç{dados.VendaId}ç{dados.Vendedor}");
            }
            catch (Exception ex)
            {
                await _applicationErrorRepository.InsertOneAsync(new ApplicationErrorCollection
                {
                    Sistema = "Processador-Relatorio",
                    DataHora = DateTime.Now,
                    Mensagem = ex.Message,
                    Stacktrace = ex.StackTrace
                });
            }
        }
    }
}

