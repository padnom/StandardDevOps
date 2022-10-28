// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using EFxceptions.Models.Exceptions;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Moq;

using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Models.Students.Exceptions;

using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRegisterIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            SqlException sqlException = GetSqlException();

            var failedStudentStorageException =
                new FailedStudentStorageException(sqlException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentStorageException);

            this.storageBrokerMock.Setup(broker => broker.InsertStudentAsync(someStudent))
                                  .Throws(sqlException);

            // when
            ValueTask<Student> registerStudentTask =
                this.studentService.RegisterStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                registerStudentTask.AsTask());
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRegisterIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            var databaseUpdateException = new DbUpdateException();

            var failedStudentStorageException =
                new FailedStudentStorageException(databaseUpdateException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentStorageException);


            this.storageBrokerMock.Setup(broker => broker.InsertStudentAsync(someStudent))
                                  .Throws(databaseUpdateException);

            // when
            ValueTask<Student> registerStudentTask =
                this.studentService.RegisterStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                registerStudentTask.AsTask());
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRegisterWhenStudentAlreadyExistsAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            string someMessage = GetRandomMessage();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistsStudentException =
                new AlreadyExistsStudentException(duplicateKeyException);

            var expectedStudentDependencyValidationException =
                new StudentDependencyValidationException(alreadyExistsStudentException);


            this.storageBrokerMock.Setup(broker => broker.InsertStudentAsync(someStudent))
                                  .Throws(duplicateKeyException);

            // when
            ValueTask<Student> registerStudentTask =
                this.studentService.RegisterStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyValidationException>(() =>
                registerStudentTask.AsTask());
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRegisterIfExceptionOccursAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            var serviceException = new Exception();

            var failedStudentServiceException =
                new FailedStudentServiceException(serviceException);

            var expectedStudentServiceException =
                new StudentServiceException(failedStudentServiceException);

            this.storageBrokerMock.Setup(broker => broker.InsertStudentAsync(someStudent))
                      .Throws(serviceException);
            // when
            ValueTask<Student> registerStudentTask =
                 this.studentService.RegisterStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentServiceException>(() =>
                registerStudentTask.AsTask());
        }
    }
}