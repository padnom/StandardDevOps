using System.Threading.Tasks;
using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Services.Foundations.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;

        public StudentService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Student> AddStudentAsync(Student student) => 
            await this.storageBroker.InsertStudentAsync(student);
    }
}
