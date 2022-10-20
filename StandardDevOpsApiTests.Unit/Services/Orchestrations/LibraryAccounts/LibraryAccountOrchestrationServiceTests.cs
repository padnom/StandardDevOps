using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.LibraryAccounts;
using StandardDevOpsApi.Models.LibraryCards;
using StandardDevOpsApi.Services.Foundations.LibraryAccounts;
using StandardDevOpsApi.Services.Foundations.LibraryCards;
using StandardDevOpsApi.Services.Foundations.LocalStudentEvents;
using StandardDevOpsApi.Services.Orchestrations.LibraryAccounts;
using Moq;
using Tynamix.ObjectFiller;

namespace StandardDevOpsApi.Tests.Unit.Services.Orchestrations.LibraryAccounts
{
    public partial class LibraryAccountOrchestrationServiceTests
    {
        private readonly Mock<ILibraryAccountService> libraryAccountServiceMock;
        private readonly Mock<ILibraryCardService> libraryCardServiceMock;
        private readonly Mock<ILocalStudentEventService> localStudentEventService;
        private readonly ILibraryAccountOrchestrationService libraryAccountOrchestrationService;

        public LibraryAccountOrchestrationServiceTests()
        {
            this.libraryAccountServiceMock = new Mock<ILibraryAccountService>(MockBehavior.Strict);
            this.libraryCardServiceMock = new Mock<ILibraryCardService>(MockBehavior.Strict);
            this.localStudentEventService = new Mock<ILocalStudentEventService>(MockBehavior.Strict);

            this.libraryAccountOrchestrationService = new LibraryAccountOrchestrationService(
                libraryAccountService: this.libraryAccountServiceMock.Object,
                libraryCardService: this.libraryCardServiceMock.Object,
                this.localStudentEventService.Object);
        }

        private static Expression<Func<LibraryCard, bool>> SameLibraryCardAs(
            LibraryCard expectedLibraryCard)
        {
            return actualLibraryCard => 
                actualLibraryCard.LibraryAccountId == expectedLibraryCard.LibraryAccountId
                && actualLibraryCard.Id != Guid.Empty;
        }

        private static LibraryAccount CreateRandomLibraryAccount() =>
            CreateLibraryAccountFiller().Create();

        private static Filler<LibraryAccount> CreateLibraryAccountFiller() =>
            new Filler<LibraryAccount>();
    }
}
