using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.LocalStudentEvents
{
    public partial class LocalStudentEventServiceTests
    {
        [Fact]
        public async Task ShouldPublishStudentAsync()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            Student inputStudent = randomStudent;

            // when
            await this.localStudentEventService
                .PublishStudentAsync(inputStudent);

            // then
            this.eventBrokerMock.Verify(broker =>
                broker.PublishStudentEventAsync(inputStudent),
                    Times.Once);

            this.eventBrokerMock.VerifyNoOtherCalls();
        }
    }
}
