using Elastic.Clients.Elasticsearch;

using StandardDevOpsApi.Models.Students;

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
                     .DefaultIndex("studen")
                     .RequestTimeout(TimeSpan.FromSeconds(300))
                     .EnableDebugMode()
                     .PrettyJson();
        }

        public async ValueTask<Student> InsertStudentAsync(Student student)
        {

            var response = await elasticsearchClient.IndexAsync(student, request => request.Index("my-student-index"));

            if (response.IsValid)
            {
                Console.WriteLine($"Index document with ID {response.Id} succeeded.");
            }

            return student;
        }
    }
}
