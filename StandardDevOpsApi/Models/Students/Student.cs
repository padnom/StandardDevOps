using Newtonsoft.Json;

using StandardDevOpsApi.Models.LibraryAccounts;

namespace StandardDevOpsApi.Models.Students
{
    public class Student
    {
        public string FirstName { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public LibraryAccount LibraryAccount { get; set; }
        public string Name { get; set; }
    }
}