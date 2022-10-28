using Moq;

using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Models.LibraryCards;
using StandardDevOpsApi.Services.Foundations.LibraryCards;

using Tynamix.ObjectFiller;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.LibraryCards
{
    public partial class LibraryCardServiceTests
    {
        private readonly ILibraryCardService libraryCardService;
        private readonly Mock<IStorageBroker> storageBrokerMock;

        public LibraryCardServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.libraryCardService = new LibraryCardService(
                storageBroker: storageBrokerMock.Object);
        }

        private static Filler<LibraryCard> CreateLibraryCardFiller() =>
            new Filler<LibraryCard>();

        private static LibraryCard CreateRandomLibraryAcocunt() =>
                    CreateLibraryCardFiller().Create();
    }
}