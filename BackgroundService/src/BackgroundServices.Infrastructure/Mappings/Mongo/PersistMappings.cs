namespace BackgroundServices.Infrastructure.Mappings.Mongo
{
    public static class PersistMappings
    {
        public static void Configure()
        {
            LoteCollectionMapping.Map();
            ArquivoCollectionMapping.Map();
            VendedorCollectionMapping.Map();
            ClienteCollectionMapping.Map();
            VendaCollectionMapping.Map();
            ItemCollectionMapping.Map();
            RelatorioCollectionMapping.Map();
        }
    }
}
