using System;
using System.Collections.Generic;
using StandardDevOpsApi.Models.LibraryCards;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Models.LibraryAccounts
{
    public class LibraryAccount
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public IEnumerable<LibraryCard> LibraryCards { get; set; }
    }
}
