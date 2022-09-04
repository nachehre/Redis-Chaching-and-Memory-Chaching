using MainProject.Domain;
using MainProject.Infra.ChainResponsibilitySolution;
using MainProject.InMemoryService.Services;
using MainProject.RedisService.Services;
using MainProject.RepositoryService.Services;
using System.Threading.Tasks;

namespace MainProject.Infra.StevenSolution
{
    public class WebApiRepo : GetDataInchainBase, IRepository
    {

        private readonly IBookRepository _dataRepository;
        private readonly IRedisCachedBooksRepository _redis;
        private readonly IMemoryCachedBooksRepository _memory;

        public WebApiRepo(IBookRepository dataRepository,
                         IRedisCachedBooksRepository redis,
                         IMemoryCachedBooksRepository memory)
        {
            _dataRepository = dataRepository;
            _redis = redis;
            _memory = memory;
        }
        public async override Task<Book> Get(string id)
        {
            var result = await _dataRepository.GetBookById(id);

            if (result == null)
            {
                if (Next != null)
                {
                    var data = Next.Get(id);
                    Book res = new Book
                    {
                        Id = data.Result.Id,
                        Title = data.Result.Title,
                        Author = data.Result.Author,
                        PublishDate = data.Result.PublishDate

                    };
                    return res;
                }
            }
            Book book = new Book
            {
                Id = result.Id,
                Title = result.Title,
                Author = result.Author,
                PublishDate = result.PublishDate

            };
            _memory.OnSetBook(book);
            _redis.SetBook(book);
            return result;
        }


        public async Task<Book> GetAsync(string id)
        {
            var result = await _dataRepository.GetBookById(id);

            if (result == null)
            {
                if (Next != null)
                {
                    var data = Next.Get(id);
                    Book res = new Book
                    {
                        Id = data.Result.Id,
                        Title = data.Result.Title,
                        Author = data.Result.Author,
                        PublishDate = data.Result.PublishDate

                    };
                    return res;
                }
            }
            Book book = new Book
            {
                Id = result.Id,
                Title = result.Title,
                Author = result.Author,
                PublishDate = result.PublishDate

            };
            _memory.OnSetBook(book);
            _redis.SetBook(book);
            return result;
        }
    }
}
