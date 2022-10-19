using System.Threading.Tasks;
using StandardDevOpsApi.Models.LibraryCards;

namespace StandardDevOpsApi.Services.Foundations.LibraryCards
{
    public interface ILibraryCardService
    {
        ValueTask<LibraryCard> AddLibraryCardAsync(LibraryCard libraryCard);
    }
}
