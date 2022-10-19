using System;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Services.Foundations.LocalStudentEvents
{
    public interface ILocalStudentEventService
    {
        void ListenToStudentEvent(Func<Student, ValueTask<Student>> studentEventHandler);
        ValueTask PublishStudentAsync(Student student);
    }
}
