using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Models.LibraryCards;
using StandardDevOpsApi.Services.Foundations.LibraryCards;
using Moq;
using Tynamix.ObjectFiller;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.LibraryCards
{
    public partial class LibraryCardServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly ILibraryCardService libraryCardService;

        public LibraryCardServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.libraryCardService = new LibraryCardService(
                storageBroker: storageBrokerMock.Object);
        }

        private static LibraryCard CreateRandomLibraryAcocunt() =>
            CreateLibraryCardFiller().Create();

        private static Filler<LibraryCard> CreateLibraryCardFiller() =>
            new Filler<LibraryCard>();
    }
}
