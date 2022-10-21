using System.Threading.Tasks;

using StandardDevOpsApi.Brokers.Apis.ElasticApis;
using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Services.Foundations.Students
{
    public class StudentIndexationService : IStudentIndexationService
    {
        private readonly IElasticApiBroker elasticApiBroker;

        public StudentIndexationService(IElasticApiBroker elasticApiBroker) =>
            this.elasticApiBroker = elasticApiBroker;

        public async ValueTask<Student> AddStudentAsync(Student student) => 
            await this.elasticApiBroker.InsertStudentAsync(student);
    }
}
