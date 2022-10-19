using System;
using System.Threading;
using System.Threading.Tasks;

using StandardDevOpsApi.Models.Students;

using MassTransit;


namespace StandardDevOpsApi.Brokers.Queues
{
    public partial class QueueBrokerMassTransit
    {
        public void ListenToStudentsMTQueue(MessageHandler<Student> studentMessageHandler)
        {
            this.busBroker.ConnectReceiveEndpoint("studentsqueue", e => e.Handler(studentMessageHandler));
        }

        public async ValueTask PublishStudentToQueueAsync(Student student)
        {
            await this.busBroker.Publish<Student>(new
            {
                student.Id,
                student.Name,
                student.LibraryAccount
            });
        }       
    }
}