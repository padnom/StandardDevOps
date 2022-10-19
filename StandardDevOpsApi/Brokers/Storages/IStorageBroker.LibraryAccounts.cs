using System.Threading.Tasks;
using StandardDevOpsApi.Models.LibraryAccounts;

namespace StandardDevOpsApi.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<LibraryAccount> InsertLibraryAccountAsync(LibraryAccount libraryAccount);
    }
}
