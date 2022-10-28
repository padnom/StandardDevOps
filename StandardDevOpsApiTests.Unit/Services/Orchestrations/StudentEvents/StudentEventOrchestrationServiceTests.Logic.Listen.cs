using Moq;

using StandardDevOpsApi.Models.Students;

using Xunit;

namespace StandardDevOpsApi.Services.Orchestrations.StudentEvents
{
    public partial class StudentEventOrchestrationServiceTests
    {
        [Fact]
        public void ShouldListenAndAddStudent()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            Student incomingStudent = randomStudent;

            var mockSequence = new MockSequence();

            this.studentEventServiceMock.InSequence(mockSequence).Setup(service =>
                service.ListenToStudentEvent(It.IsAny<Func<Student, ValueTask>>()))
                    .Callback<Func<Student, ValueTask>>(eventFunction =>
                        eventFunction.Invoke(incomingStudent));

            this.studentServiceMock.InSequence(mockSequence).Setup(service =>
                service.RegisterStudentAsync(incomingStudent))
                    .ReturnsAsync(incomingStudent);

            // when
            this.studentEventOrchestrationService.ListenToStudentEvents();

            // then
            this.studentEventServiceMock.Verify(service =>
                service.ListenToStudentEvent(It.IsAny<Func<Student, ValueTask>>()),
                    Times.Once);

            this.studentServiceMock.Verify(service =>
                service.RegisterStudentAsync(incomingStudent),
                    Times.Once);

            this.localStudentEventServiceMock.Verify(broker =>
                broker.PublishStudentAsync(incomingStudent),
                    Times.Once);

            this.studentEventServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
            this.localStudentEventServiceMock.VerifyNoOtherCalls();
        }
    }
}