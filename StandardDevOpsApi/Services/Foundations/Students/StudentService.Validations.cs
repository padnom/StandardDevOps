// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Models.Students.Exceptions;

namespace StandardDevOpsApi.Services.Foundations.Students
{
    public partial class StudentService
    {
        public void ValidateAgainstStorageStudentOnModify(Student inputStudent, Student storageStudent)
        {

        }

        private static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);

        private static bool IsInvalid(Guid input) => input == default;

        private static dynamic IsInvalidX(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalidX(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalidX(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentException = new InvalidStudentException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentException.ThrowIfContainsErrors();
        }

        private static void ValidateStorageStudent(Student storageStudent, Guid studentId)
        {
            if (storageStudent is null)
            {
                throw new NotFoundStudentException(studentId);
            }
        }

        private static void ValidateStudent(Student student)
        {
            if (student is null)
            {
                throw new NullStudentException();
            }
        }

        private static void ValidateStudentId(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                throw new InvalidStudentException(
                    parameterName: nameof(Student.Id),
                    parameterValue: studentId);
            }
        }

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTime();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }

        private dynamic IsNotRecent(DateTimeOffset dateTimeOffset) => new
        {
            Condition = IsDateNotRecent(dateTimeOffset),
            Message = "Date is not recent"
        };

        private void ValidateStudentOnModify(Student student)
        {
            ValidateStudent(student);

            Validate(
                (Rule: IsInvalidX(student.Id), Parameter: nameof(Student.Id)),
                (Rule: IsInvalidX(student.FirstName), Parameter: nameof(Student.FirstName))
                );
        }

        private void ValidateStudentOnRegister(Student student)
        {
            ValidateStudent(student);

            Validate(
                (Rule: IsInvalidX(student.Id), Parameter: nameof(Student.Id)),
                (Rule: IsInvalidX(student.FirstName), Parameter: nameof(Student.FirstName))
            );
        }
    }
}