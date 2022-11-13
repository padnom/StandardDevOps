using Elastic.Clients.Elasticsearch;

namespace StandardDevOpsApi.Brokers.Apis.ElasticApis
{
    public partial class ElasticApiBroker : IElasticApiBroker
    {
        private readonly ElasticsearchClient elasticsearchClient;

        public ElasticApiBroker(IConfiguration configuration)
        {
            ElasticsearchClientSettings elasticsearchClientSettings = ConfigureElasticSearch(configuration);
            this.elasticsearchClient = new ElasticsearchClient(elasticsearchClientSettings);
        }

        private ElasticsearchClientSettings ConfigureElasticSearch(IConfiguration configuration)
        {
            return new ElasticsearchClientSettings(new Uri(configuration.GetConnectionString("ElasticsearchConnection")))
                     .RequestTimeout(TimeSpan.FromSeconds(300))
                     .EnableDebugMode()
                     .PrettyJson();
        }
    }
}