using MainProject.Domain;
using System.Threading.Tasks;

namespace MainProject.Infra.Steven_Solution
{
    public class CachedRepository : IRepository
    {

        private readonly IRepository _inMemory;
        private readonly IRepository _redis;
        private readonly IRepository _webApi;

        public CachedRepository(IRepository inMemory, IRepository redis, IRepository webApi)
        {
           
            _inMemory = inMemory;
            _redis = redis;
            _webApi = webApi;
        }

        public async Task<Book> GetAsync(string id)
        {
            var book = await _inMemory.GetAsync(id);
            if (book !=null)
            {
                return book;
            }
            book = await _redis.GetAsync(id);
            return book is not null ? book : await _webApi.GetAsync(id);
        }
    }
}
