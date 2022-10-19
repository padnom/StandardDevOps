using System;
using System.Threading;
using System.Threading.Tasks;

using StandardDevOpsApi.Models.Students;

using MassTransit;

using Microsoft.Azure.ServiceBus;

namespace StandardDevOpsApi.Brokers.Queues
{
    public partial interface IQueueBroker
    {
        void ListenToStudentsQueue(Func<Message, CancellationToken, Task> eventHandler);
    }
}
