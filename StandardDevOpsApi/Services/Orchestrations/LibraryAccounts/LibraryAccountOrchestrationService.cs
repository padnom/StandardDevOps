using System;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.LibraryAccounts;
using StandardDevOpsApi.Models.LibraryCards;
using StandardDevOpsApi.Services.Foundations.LibraryAccounts;
using StandardDevOpsApi.Services.Foundations.LibraryCards;
using StandardDevOpsApi.Services.Foundations.LocalStudentEvents;

namespace StandardDevOpsApi.Services.Orchestrations.LibraryAccounts
{
    public class LibraryAccountOrchestrationService : ILibraryAccountOrchestrationService
    {
        private readonly ILibraryAccountService libraryAccountService;
        private readonly ILibraryCardService libraryCardService;
        private readonly ILocalStudentEventService localStudentEventService;

        public LibraryAccountOrchestrationService(
            ILibraryAccountService libraryAccountService,
            ILibraryCardService libraryCardService,
            ILocalStudentEventService localStudentEventService)
        {
            this.libraryAccountService = libraryAccountService;
            this.libraryCardService = libraryCardService;
            this.localStudentEventService = localStudentEventService;
        }

        public void ListenToLocalStudentEvent()
        {
            this.localStudentEventService.ListenToStudentEvent(async (student) =>
            {
                var libraryAccount = new LibraryAccount
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.Id
                };

                await CreateLibraryAccountAsync(libraryAccount);

                return student;
            });
        }

        public async ValueTask<LibraryAccount> CreateLibraryAccountAsync(LibraryAccount libraryAccount)
        {
            LibraryAccount addedLibraryAccount =
                await this.libraryAccountService
                    .AddLibraryAccountAsync(libraryAccount);

            await CreateLibraryCardAsync(libraryAccount);

            return addedLibraryAccount;
        }

        private async Task CreateLibraryCardAsync(LibraryAccount libraryAccount)
        {
            LibraryCard inputLibraryCard =
                CreateLibraryCard(libraryAccount.Id);

            await this.libraryCardService
                .AddLibraryCardAsync(inputLibraryCard);
        }

        private static LibraryCard CreateLibraryCard(Guid libraryAccountId)
        {
            return new LibraryCard
            {
                Id = Guid.NewGuid(),
                LibraryAccountId = libraryAccountId
            };
        }
    }
}
