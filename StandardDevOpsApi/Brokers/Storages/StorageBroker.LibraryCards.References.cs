using StandardDevOpsApi.Models.LibraryCards;
using Microsoft.EntityFrameworkCore;

namespace StandardDevOpsApi.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void AddLibraryCardsReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryCard>()
                .HasOne(libraryCard => libraryCard.LibraryAccount)
                .WithMany(account => account.LibraryCards)
                .HasForeignKey(libraryCard => libraryCard.LibraryAccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
