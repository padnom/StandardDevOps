using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Apis.ElasticApis
{
    public partial class ElasticApiBroker : IElasticApiBroker
    {
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