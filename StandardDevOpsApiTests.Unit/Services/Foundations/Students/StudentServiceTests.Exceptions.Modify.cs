// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

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
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            int randomDays = GetRandomNumber();

            SqlException sqlException = GetSqlException();

            var failedStudentStorageException =
                new FailedStudentStorageException(sqlException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentStorageException);

            this.storageBrokerMock.Setup(broker => broker.SelectStudentByIdAsync(someStudent.Id))
                      .ReturnsAsync(someStudent);

            this.storageBrokerMock.Setup(broker => broker.UpdateStudentAsync(someStudent))
                .Throws(sqlException);

            // when
            ValueTask<Student> modifyStudentTask =
                this.studentService.ModifyStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                modifyStudentTask.AsTask());
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDbUpdateConcurrencyExceptionOccursAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            int randomDays = GetRandomNumber();

            var databaseUpdateConcurrencyException =
                new DbUpdateConcurrencyException();

            var lockedStudentException =
                new LockedStudentException(databaseUpdateConcurrencyException);

            var expectedStudentDependencyException =
                new StudentDependencyException(lockedStudentException);

            this.storageBrokerMock.Setup(broker => broker.SelectStudentByIdAsync(someStudent.Id))
                                  .ReturnsAsync(someStudent);

            this.storageBrokerMock.Setup(broker => broker.UpdateStudentAsync(someStudent))
                .Throws(databaseUpdateConcurrencyException);

            // when
            ValueTask<Student> modifyStudentTask =
                this.studentService.ModifyStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                modifyStudentTask.AsTask());

        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDbUpdateExceptionOccursAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            int randomDays = GetRandomNumber();

            var databaseUpdateException = new DbUpdateException();

            var failedStudentStorageException =
                new FailedStudentStorageException(databaseUpdateException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentStorageException);

            this.storageBrokerMock.Setup(broker => broker.SelectStudentByIdAsync(someStudent.Id))
                      .ReturnsAsync(someStudent);

            this.storageBrokerMock.Setup(broker => broker.UpdateStudentAsync(someStudent))
                .Throws(databaseUpdateException);

            // when
            ValueTask<Student> modifyStudentTask =
                this.studentService.ModifyStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                modifyStudentTask.AsTask());
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfServiceExceptionOccursAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            int randomDays = GetRandomNumber();

            var serviceException = new Exception();

            var failedStudentServiceException =
                new FailedStudentServiceException(serviceException);

            var expectedStudentServiceException =
                new StudentServiceException(failedStudentServiceException);

            this.storageBrokerMock.Setup(broker => broker.SelectStudentByIdAsync(someStudent.Id))
                      .ReturnsAsync(someStudent);

            this.storageBrokerMock.Setup(broker => broker.UpdateStudentAsync(someStudent))
                .Throws(serviceException);

            // when
            ValueTask<Student> modifyStudentTask =
                this.studentService.ModifyStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentServiceException>(() =>
                modifyStudentTask.AsTask()); 
        }
    }
}