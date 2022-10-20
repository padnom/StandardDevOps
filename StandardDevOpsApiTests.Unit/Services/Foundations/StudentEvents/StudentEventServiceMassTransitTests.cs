using System;
using System.Linq.Expressions;
using System.Text;
using StandardDevOpsApi.Brokers.Queues;
using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Services.Foundations.StudentEvents;
using KellermanSoftware.CompareNetObjects;

using MassTransit;

using Moq;
using Newtonsoft.Json;
using Tynamix.ObjectFiller;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.StudentEvents
{
    public partial class StudentEventServiceMassTransitTests
    {
        private readonly Mock<IQueueBrokerMassTransit> queueBrokerMock;
        private readonly IStudentEventService studentEventService;
        private readonly ICompareLogic comparelogic;

        public StudentEventServiceMassTransitTests()
        {
            this.queueBrokerMock = new Mock<IQueueBrokerMassTransit>();
            this.comparelogic = new CompareLogic();

            this.studentEventService = new StudentEventServiceMassTransit(
                queueBroker: this.queueBrokerMock.Object);
        }

        private static ConsumeContext<Student> CreateStudentMessageHandler(Student student)
        {
            string serializedStudent = JsonConvert.SerializeObject(student);
            byte[] studentBody = Encoding.UTF8.GetBytes(serializedStudent);

            var context = Mock.Of<ConsumeContext<Student>>(_ =>
                _.Message == student);
            return context;
        }

        private Expression<Func<Student, bool>> SameStudentAs(Student expectedStudent)
        {
            return actualStudent =>
                this.comparelogic.Compare(expectedStudent, actualStudent).AreEqual;
        }

        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}
