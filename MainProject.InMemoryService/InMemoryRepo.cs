using MainProject.Domain;

using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace MainProject.Infra.StevenSolution
{
    public class InMemoryRepo :  IRepository
    {

        private readonly IRepository _decorateRepository;
        private readonly IMemoryCache _memoryCache;

        public InMemoryRepo(IRepository decorateRepository,IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _decorateRepository = decorateRepository;

        }
        public async Task<Book> GetAsync(string id)
        {
            Book book = new Book();

            var result = _memoryCache.Get(id);

            if (result == null)
            {
                _decorateRepository.GetAsync(id);
                return null;
            }
            book = (Book)result;
            return book;
        }

        
    }
}
