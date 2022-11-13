using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Caches
{
    public partial interface ICacheBroker
    {
        ValueTask<Student> DeleteStudentAsync(Student student);
        ValueTask<Student> InsertStudentAsync(Student student);
        ValueTask<IEnumerable<Student>> SelectAllStudentsAsync();
        ValueTask<Student> SelectStudentByIdAsync(string studentId);
        ValueTask<Student> UpdateStudentAsync(Student student);
    }
}