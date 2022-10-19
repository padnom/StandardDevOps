using System.Threading.Tasks;
using StandardDevOpsApi.Models.LibraryAccounts;

namespace StandardDevOpsApi.Services.Foundations.LibraryAccounts
{
    public interface ILibraryAccountService
    {
        ValueTask<LibraryAccount> AddLibraryAccountAsync(LibraryAccount libraryAccount);
    }
}
