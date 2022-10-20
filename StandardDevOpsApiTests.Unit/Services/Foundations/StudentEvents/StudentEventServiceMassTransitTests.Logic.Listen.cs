using System;
using System.Threading;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;
using MassTransit;

using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.StudentEvents
{
    public partial class StudentEventServiceMassTransitTests
    {
        [Fact]
        public void ShouldListenToStudentEvent()
        {
            // given
            var studentEventHandlerMock =
                new Mock<Func<Student, ValueTask>>();

            Student randomStudent = CreateRandomStudent();
            Student incomingStudent = randomStudent;

            ConsumeContext<Student> studentMessageHandler =
                CreateStudentMessageHandler(incomingStudent);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToStudentsMTQueue(It.IsAny<MessageHandler<Student>>()))
                .Callback<MessageHandler<Student>>(eventFunction => eventFunction.Invoke(studentMessageHandler));

            // when
            studentEventService.ListenToStudentEvent(
                studentEventHandler: studentEventHandlerMock.Object);

            // then
            studentEventHandlerMock.Verify(handler =>
                handler.Invoke(It.Is(SameStudentAs(incomingStudent))),
                    Times.Once());

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToStudentsMTQueue(
                    It.IsAny<MessageHandler<Student>>()),
                        Times.Once());

            this.queueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
