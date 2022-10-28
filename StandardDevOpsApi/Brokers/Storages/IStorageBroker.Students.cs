using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Student> DeleteStudentAsync(Student student);

        ValueTask<Student> InsertStudentAsync(Student student);

        IQueryable<Student> SelectAllStudents();

        ValueTask<Student> SelectStudentByIdAsync(Guid studentId);

        ValueTask<Student> UpdateStudentAsync(Student student);
    }
}