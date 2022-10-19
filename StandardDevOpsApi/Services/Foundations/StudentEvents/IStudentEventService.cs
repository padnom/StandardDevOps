using System;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Services.Foundations.StudentEvents
{
    public interface IStudentEventService
    {
        void ListenToStudentEvent(Func<Student, ValueTask> studentEventHandler);
        ValueTask PublishStudentToQueueAsync(Student student);
    }
}
