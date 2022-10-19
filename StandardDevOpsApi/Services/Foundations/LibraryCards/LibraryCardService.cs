using System.Threading.Tasks;
using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Models.LibraryCards;

namespace StandardDevOpsApi.Services.Foundations.LibraryCards
{
    public class LibraryCardService : ILibraryCardService
    {
        private readonly IStorageBroker storageBroker;

        public LibraryCardService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<LibraryCard> AddLibraryCardAsync(LibraryCard libraryCard) =>
            await this.storageBroker.InsertLibraryCardAsync(libraryCard);
    }
}
