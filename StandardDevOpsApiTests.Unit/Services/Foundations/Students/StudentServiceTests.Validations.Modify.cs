// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Force.DeepCloner;

using Moq;

using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Models.Students.Exceptions;

using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfCreatedAndUpdatedDatesAreSameAndLogItAsync()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            Student invalidStudent = randomStudent;

            var invalidStudentException = new InvalidStudentException();

            var expectedStudentValidationException =
                new StudentValidationException(invalidStudentException);

            // when
            ValueTask<Student> modifyStudentTask =
                this.studentService.ModifyStudentAsync(invalidStudent);

            // then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                modifyStudentTask.AsTask());

        }
       

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfStudentDoesntExistAndLogItAsync()
        {
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Student randomStudent = CreateRandomStudent();
            Student nonExistentStudent = randomStudent;
            Student noStudent = null;
            var notFoundStudentException = new NotFoundStudentException(nonExistentStudent.Id);

            var expectedStudentValidationException =
                new StudentValidationException(notFoundStudentException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectStudentByIdAsync(nonExistentStudent.Id))
                    .ReturnsAsync(noStudent);

            // when
            ValueTask<Student> modifyStudentTask =
                this.studentService.ModifyStudentAsync(nonExistentStudent);

            // then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                modifyStudentTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectStudentByIdAsync(nonExistentStudent.Id),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentValidationException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnModifyWhenStudentIsInvalidAndLogItAsync(string invalidText)
        {
            // given
            var invalidStudent = new Student
            {
                FirstName = invalidText
            };

            var invalidStudentException = new InvalidStudentException();

            invalidStudentException.AddData(
                key: nameof(Student.Id),
                values: "Id is required");

            var expectedStudentValidationException =
                new StudentValidationException(invalidStudentException);

            // when
            ValueTask<Student> modifyStudentTask =
                this.studentService.ModifyStudentAsync(invalidStudent);

            // then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                modifyStudentTask.AsTask());


        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyWhenStudentIsNullAndLogItAsync()
        {
            // given
            Student invalidStudent = null;
            var nullStudentException = new NullStudentException();

            var expectedStudentValidationException =
                new StudentValidationException(nullStudentException);

            // when
            ValueTask<Student> modifyStudentTask =
                this.studentService.ModifyStudentAsync(invalidStudent);

            // then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                modifyStudentTask.AsTask());

        }
    }
}