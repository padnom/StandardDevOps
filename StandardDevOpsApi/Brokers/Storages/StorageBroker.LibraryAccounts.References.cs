using StandardDevOpsApi.Models.LibraryAccounts;
using Microsoft.EntityFrameworkCore;

namespace StandardDevOpsApi.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void AddLibraryAccountsReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryAccount>()
                .HasOne(libraryAccount => libraryAccount.Student)
                .WithOne(student => student.LibraryAccount)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
