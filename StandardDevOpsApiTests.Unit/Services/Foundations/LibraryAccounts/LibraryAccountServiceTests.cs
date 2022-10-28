using Moq;

using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Models.LibraryAccounts;
using StandardDevOpsApi.Services.Foundations.LibraryAccounts;

using Tynamix.ObjectFiller;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.LibraryAccounts
{
    public partial class LibraryAccountServiceTests
    {
        private readonly ILibraryAccountService libraryAccountService;
        private readonly Mock<IStorageBroker> storageBrokerMock;

        public LibraryAccountServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.libraryAccountService = new LibraryAccountService(
                storageBroker: storageBrokerMock.Object);
        }

        private static Filler<LibraryAccount> CreateLibraryAccountFiller() =>
            new Filler<LibraryAccount>();

        private static LibraryAccount CreateRandomLibraryAcocunt() =>
                    CreateLibraryAccountFiller().Create();
    }
}