using MainProject.Domain;
using MainProject.InMemoryService.Services;
using MainProject.RedisService.Services;
using System.Threading.Tasks;

namespace MainProject.Infra.ChainResponsibilitySolution
{
    public class MemoryGetter : GetDataInchainBase
    {

        private readonly IMemoryCachedBooksRepository _memory;

        public MemoryGetter(IMemoryCachedBooksRepository memory)
        {

            _memory = memory;
        }
        public async override Task<Book> Get(string id)
        {
            var result = await _memory.OnGetBook(id);

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


            }

            return result;

        }
    }
}
