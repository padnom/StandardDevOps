using System.Threading.Tasks;
using StandardDevOpsApi.Models.LibraryAccounts;

namespace StandardDevOpsApi.Services.Orchestrations.LibraryAccounts
{
    public interface ILibraryAccountOrchestrationService
    {
        ValueTask<LibraryAccount> CreateLibraryAccountAsync(LibraryAccount libraryAccount);
        void ListenToLocalStudentEvent();
    }
}
