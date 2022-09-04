using MainProject.Domain;
using MainProject.Infra.ChainResponsibilitySolution;
using MainProject.InMemoryService.Services;
using MainProject.RedisService.Services;
using System.Threading.Tasks;

namespace MainProject.Infra.StevenSolution
{
    public class RedisRepo : GetDataInchainBase,IRepository
    {

        private readonly IRedisCachedBooksRepository _redis;
        private readonly IMemoryCachedBooksRepository _memory;

        public RedisRepo(IRedisCachedBooksRepository redis,
                         IMemoryCachedBooksRepository memory)
        {

            _redis = redis;
            _memory = memory;
        }
        public async override Task<Book> Get(string id)
        {
            var result = await _redis.GetBook(id);
            if (result == null)
            {
                if (Next != null)
                {
                    Book book = new Book();
                    var data = Next.Get(id);
                    book.Id = data.Result.Id;
                    book.Title = data.Result.Title;
                    book.Author = data.Result.Author;
                    book.PublishDate = data.Result.PublishDate;
                    return book;
                }
                else
                {
                    return null;
                }
            }
            _memory.OnSetBook(result);

            return result;
        }

        public async Task<Book> GetAsync(string id)
        {
            var result = await _redis.GetBook(id);
            if (result == null)
            {
                if (Next != null)
                {
                    Book book = new Book();
                    var data = Next.Get(id);
                    book.Id = data.Result.Id;
                    book.Title = data.Result.Title;
                    book.Author = data.Result.Author;
                    book.PublishDate = data.Result.PublishDate;
                    return book;
                }
                else
                {
                    return null;
                }
            }
            _memory.OnSetBook(result);

            return result;
        }

     
    }
}
