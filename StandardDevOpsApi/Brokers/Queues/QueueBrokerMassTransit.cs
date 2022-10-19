using MassTransit;

using Microsoft.Extensions.Configuration;

namespace StandardDevOpsApi.Brokers.Queues
{
    public partial class QueueBrokerMassTransit : IQueueBrokerMassTransit
    {
        private readonly IConfiguration configuration;
        private IBusControl busBroker;

        public QueueBrokerMassTransit(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.busBroker = ConfigureBus();
        }

        private IBusControl ConfigureBus()
        {
            var massTransitSection = this.configuration.GetSection("MassTransit");
            var url = massTransitSection.GetValue<string>("Url");
            var host = massTransitSection.GetValue<string>("Host");
            var userName = massTransitSection.GetValue<string>("UserName");
            var password = massTransitSection.GetValue<string>("Password");
            var busControl =
                Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                    {
                        configurator.Username(userName);
                        configurator.Password(password);
                    });
                    cfg.PublishTopology.BrokerTopologyOptions = PublishBrokerTopologyOptions.MaintainHierarchy;
                });
            busControl.StartAsync();
            return busControl;
        }
    }
}