using System;
using System.Collections;
using System.Collections.Generic;
using StandardDevOpsApi.Models.LibraryAccounts;

namespace StandardDevOpsApi.Models.Students
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public LibraryAccount LibraryAccount { get; set; }
    }
}
