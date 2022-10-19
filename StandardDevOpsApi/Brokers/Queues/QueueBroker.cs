using System.Threading.Tasks;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace StandardDevOpsApi.Brokers.Queues
{
    public partial class QueueBroker : IQueueBroker
    {
        private readonly IConfiguration configuration;

        public QueueBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            InitializeQueueClients();
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            ExceptionReceivedContext context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            return Task.CompletedTask;
        }

        private MessageHandlerOptions GetMessageHandlerOptions()
        {
            return new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            };
        }

        private IQueueClient GetQueueClient(string queueName)
        {
            string connectionString =
                this.configuration.GetConnectionString("ServiceBusConnection");

            return new QueueClient(connectionString, queueName);
        }

        private void InitializeQueueClients() =>
                                    this.StudentsQueue = GetQueueClient(nameof(this.StudentsQueue));
    }
}