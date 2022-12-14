using System.Threading.Tasks;

using StandardDevOpsApi.Brokers.Caches;
using StandardDevOpsApi.Brokers.DateTimes;
using StandardDevOpsApi.Brokers.Loggings;
using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Services.Foundations.Students
{
    public partial class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ICacheBroker cacheBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentService(
            IStorageBroker storageBroker,
            ICacheBroker cacheBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.cacheBroker = cacheBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Student> RegisterStudentAsync(Student student) =>
        TryCatch(async () =>
        {
            ValidateStudentOnRegister(student);
            Student studentInserted = await this.storageBroker.InsertStudentAsync(student);
            await this.cacheBroker.InsertStudentAsync(studentInserted);
            return studentInserted;
        });

        public IQueryable<Student> RetrieveAllStudents() =>
        TryCatch(() => this.storageBroker.SelectAllStudents());

        public ValueTask<Student> RetrieveStudentByIdAsync(Guid studentId) =>
        TryCatch(async () =>
        {
            ValidateStudentId(studentId);

            Student studenMaybeCached = await this.cacheBroker.SelectStudentByIdAsync(studentId.ToString());
            if (studenMaybeCached is not null)
            {
                ValidateStorageStudent(studenMaybeCached, studentId);
                return studenMaybeCached;

            }

            Student maybeStudent = await this.storageBroker.SelectStudentByIdAsync(studentId);
            ValidateStorageStudent(maybeStudent, studentId);

            return maybeStudent;
        });

        public ValueTask<Student> ModifyStudentAsync(Student student) =>
        TryCatch(async () =>
        {
            ValidateStudentOnModify(student);

            Student maybeStudent =
                await this.storageBroker.SelectStudentByIdAsync(student.Id);

            ValidateStorageStudent(maybeStudent, student.Id);

            return await this.storageBroker.UpdateStudentAsync(student);
        });



        public ValueTask<Student> RemoveStudentByIdAsync(Guid studentId) =>
        TryCatch(async () =>
        {
            ValidateStudentId(studentId);

            Student maybeStudent =
                await this.storageBroker.SelectStudentByIdAsync(studentId);

            ValidateStorageStudent(maybeStudent, studentId);

            return await this.storageBroker.DeleteStudentAsync(maybeStudent);
        });

    }
}

