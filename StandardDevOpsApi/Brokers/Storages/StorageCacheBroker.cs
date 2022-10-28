using Microsoft.Extensions.Caching.Memory;

using StandardDevOpsApi.Models.LibraryAccounts;
using StandardDevOpsApi.Models.LibraryCards;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Storages
{
    public class StorageCacheBroker : IStorageBroker
    {
        private readonly IStorageBroker storageBroker;
        private readonly IMemoryCache memoryCache;


        public StorageCacheBroker(IStorageBroker storageBroker, IMemoryCache memoryCache)
        {
            this.storageBroker = storageBroker;
            this.memoryCache = memoryCache;
        }

        public ValueTask<Student> DeleteStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public ValueTask<LibraryAccount> InsertLibraryAccountAsync(LibraryAccount libraryAccount)
        {
            
            throw new NotImplementedException();

        }

        public ValueTask<LibraryCard> InsertLibraryCardAsync(LibraryCard libraryCard)
        {
            //string key = $"member-{libraryCard.Id}";
            //memoryCache.Set
            //storageBroker.InsertLibraryCardAsync(libraryCard);
            return new ValueTask<LibraryCard>(libraryCard);
        }

        public ValueTask<Student> InsertStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> SelectAllStudents()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Student> SelectStudentByIdAsync(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Student> UpdateStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
