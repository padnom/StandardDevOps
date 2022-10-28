using Moq;

using StandardDevOpsApi.Brokers.Events;
using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Services.Foundations.LocalStudentEvents;

using Tynamix.ObjectFiller;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.LocalStudentEvents
{
    public partial class LocalStudentEventServiceTests
    {
        private readonly Mock<IEventBroker> eventBrokerMock;
        private readonly ILocalStudentEventService localStudentEventService;

        public LocalStudentEventServiceTests()
        {
            this.eventBrokerMock = new Mock<IEventBroker>();

            this.localStudentEventService = new LocalStudentEventService(
                eventBroker: this.eventBrokerMock.Object);
        }

        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}