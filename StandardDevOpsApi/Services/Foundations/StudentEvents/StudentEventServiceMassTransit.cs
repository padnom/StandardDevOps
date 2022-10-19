using System;
using System.Threading.Tasks;

using StandardDevOpsApi.Brokers.Queues;
using StandardDevOpsApi.Models.Students;


namespace StandardDevOpsApi.Services.Foundations.StudentEvents
{
    public partial class StudentEventServiceMassTransit : IStudentEventService
    {
        private readonly IQueueBrokerMassTransit queueBroker;

        public StudentEventServiceMassTransit(IQueueBrokerMassTransit queueBroker) =>
            this.queueBroker = queueBroker;

        public void ListenToStudentEvent(Func<Student, ValueTask> studentEventHandler)
        {
            this.queueBroker.ListenToStudentsMTQueue(async (messageHandler) =>
            {
                Student incomingStudent = messageHandler.Message;
                await studentEventHandler(incomingStudent);
            });
        }

        public async ValueTask PublishStudentToQueueAsync(Student student)
        {
            await this.queueBroker.PublishStudentToQueueAsync(student);
        }
       
    }
}