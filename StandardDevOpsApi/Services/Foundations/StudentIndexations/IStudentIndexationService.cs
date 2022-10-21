using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Services.Foundations.Students
{
    public interface IStudentIndexationService
    {
        ValueTask<Student> AddStudentAsync(Student student);
    }
}
