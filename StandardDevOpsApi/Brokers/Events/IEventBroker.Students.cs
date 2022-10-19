using System;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Events
{
    public partial interface IEventBroker
    {
        void ListenToStudentEvent(Func<Student, ValueTask<Student>> studentEventHandler);
        ValueTask PublishStudentEventAsync(Student student);
    }
}
