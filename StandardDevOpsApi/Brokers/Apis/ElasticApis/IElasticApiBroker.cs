using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Apis.ElasticApis
{
    public interface IElasticApiBroker
    {
        ValueTask<Student> InsertStudentAsync(Student student);
    }
}