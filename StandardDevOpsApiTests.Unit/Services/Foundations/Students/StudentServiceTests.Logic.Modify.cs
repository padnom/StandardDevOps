// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using FluentAssertions;

using Force.DeepCloner;

using Moq;

using StandardDevOpsApi.Models.Students;

using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldModifyStudentAsync()
        {
            // given
            int randomNumber = GetRandomNumber();
            int randomDays = randomNumber;
            DateTimeOffset randomDate = GetRandomDateTime();
            Student randomStudent = CreateRandomStudent();
            Student inputStudent = randomStudent;
            Student afterUpdateStorageStudent = inputStudent;
            Student expectedStudent = afterUpdateStorageStudent;
            Student beforeUpdateStorageStudent = randomStudent.DeepClone();

            Guid studentId = inputStudent.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectStudentByIdAsync(studentId))
                    .ReturnsAsync(beforeUpdateStorageStudent);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateStudentAsync(inputStudent))
                    .ReturnsAsync(afterUpdateStorageStudent);

            // when
            Student actualStudent = await this.studentService.ModifyStudentAsync(inputStudent);

            // then
            actualStudent.Should().BeEquivalentTo(expectedStudent);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectStudentByIdAsync(studentId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateStudentAsync(inputStudent),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}