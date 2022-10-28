using StandardDevOpsApi.Models.LibraryAccounts;

namespace StandardDevOpsApi.Models.Students
{
    public class Student
    {
        public string FirstName { get; set; }
        public Guid Id { get; set; }
        public LibraryAccount LibraryAccount { get; set; }
        public string Name { get; set; }
    }
}