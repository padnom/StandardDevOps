using System.Threading.Tasks;

using StandardDevOpsApi.Models.Students;
using MassTransit;

namespace StandardDevOpsApi.Brokers.Queues
{
    public partial interface IQueueBrokerMassTransit
    {
        void ListenToStudentsMTQueue(MessageHandler<Student> studentMessageHandler);
        ValueTask PublishStudentToQueueAsync(Student student);

    }
}