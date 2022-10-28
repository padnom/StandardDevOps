// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using FluentAssertions;

using Moq;

using StandardDevOpsApi.Models.Students;

using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveStudentByIdAsync()
        {
            // given
            Guid randomStudentId = Guid.NewGuid();
            Guid inputStudentId = randomStudentId;
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Student randomStudent = CreateRandomStudent(randomDateTime);
            Student storageStudent = randomStudent;
            Student expectedStudent = storageStudent;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectStudentByIdAsync(inputStudentId))
                    .ReturnsAsync(storageStudent);

            // when
            Student actualStudent =
                await this.studentService.RetrieveStudentByIdAsync(inputStudentId);

            // then
            actualStudent.Should().BeEquivalentTo(expectedStudent);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectStudentByIdAsync(inputStudentId),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}