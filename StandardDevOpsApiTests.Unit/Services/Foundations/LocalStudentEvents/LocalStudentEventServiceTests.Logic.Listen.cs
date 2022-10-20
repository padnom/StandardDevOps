using System;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;
using Moq;
using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.LocalStudentEvents
{
    public partial class LocalStudentEventServiceTests
    {
        [Fact]
        public void ShouldListenToStudentEvent()
        {
            // given
            var studentEventHandlerMock =
                new Mock<Func<Student, ValueTask<Student>>>();

            // when
            this.localStudentEventService.ListenToStudentEvent(
                studentEventHandlerMock.Object);

            // then
            this.eventBrokerMock.Verify(broker =>
                broker.ListenToStudentEvent(
                    studentEventHandlerMock.Object),
                        Times.Once);

            this.eventBrokerMock.VerifyNoOtherCalls();
        }
    }
}
