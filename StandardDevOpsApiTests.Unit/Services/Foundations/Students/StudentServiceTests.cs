// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Linq.Expressions;
using System.Runtime.Serialization;

using Microsoft.Data.SqlClient;

using Moq;

using StandardDevOpsApi.Brokers.Caches;
using StandardDevOpsApi.Brokers.DateTimes;
using StandardDevOpsApi.Brokers.Loggings;
using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Services.Foundations.Students;

using Tynamix.ObjectFiller;

using Xeptions;

using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ICacheBroker> cacheBrokerMock;
        private readonly IStudentService studentService;

        public StudentServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.cacheBrokerMock = new Mock<ICacheBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentService = new StudentService(
                storageBroker: this.storageBrokerMock.Object,
                cacheBroker: this.cacheBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData InvalidMinuteCases()
        {
            int randomMoreThanMinuteFromNow = GetRandomNumber();
            int randomMoreThanMinuteBeforeNow = GetNegativeRandomNumber();

            return new TheoryData<int>
            {
                randomMoreThanMinuteFromNow ,
                randomMoreThanMinuteBeforeNow
            };
        }

        private static Student CreateRandomStudent() =>
                    CreateStudentFiller(dates: DateTimeOffset.UtcNow).Create();

        private static Student CreateRandomStudent(DateTimeOffset dates) =>
            CreateStudentFiller(dates).Create();

        private static IQueryable<Student> CreateRandomStudents(DateTimeOffset dates) =>
            CreateStudentFiller(dates).Create(GetRandomNumber()).AsQueryable();

        private static Filler<Student> CreateStudentFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Student>();
            Guid createdById = Guid.NewGuid();

            filler.Setup();

            return filler;
        }

        private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string GetRandomMessage() => new MnemonicString().GetValue();

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static Expression<Func<Exception, bool>> SameValidationExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }
    }
}