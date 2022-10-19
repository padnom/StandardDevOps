using System.Threading.Tasks;
using StandardDevOpsApi.Models.LibraryCards;

namespace StandardDevOpsApi.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<LibraryCard> InsertLibraryCardAsync(LibraryCard libraryCard);
    }
}
