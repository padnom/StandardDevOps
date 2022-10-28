using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Services.Foundations.Students
{
    public interface IStudentService
    {
        ValueTask<Student> RegisterStudentAsync(Student student);
        IQueryable<Student> RetrieveAllStudents();
        ValueTask<Student> RetrieveStudentByIdAsync(Guid studentId);
        ValueTask<Student> ModifyStudentAsync(Student student);
        ValueTask<Student> RemoveStudentByIdAsync(Guid studentId);
    }
}
