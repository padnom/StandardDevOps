using System;
using StandardDevOpsApi.Models.LibraryAccounts;

namespace StandardDevOpsApi.Models.LibraryCards
{
    public class LibraryCard
    {
        public Guid Id { get; set; }

        public Guid LibraryAccountId { get; set; }
        public LibraryAccount LibraryAccount { get; set; }
    }
}
