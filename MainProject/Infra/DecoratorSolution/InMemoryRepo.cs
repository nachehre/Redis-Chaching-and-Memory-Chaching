using MainProject.Domain;
using MainProject.Infra.ChainResponsibilitySolution;
using MainProject.InMemoryService.Services;
using System.Threading.Tasks;

namespace MainProject.Infra.StevenSolution
{
    public class InMemoryRepo : GetDataInchainBase, IRepository
    {

        private readonly IMemoryCachedBooksRepository _memory;

        public InMemoryRepo(IMemoryCachedBooksRepository memory)
        {

            _memory = memory;
        }
        public async Task<Book> GetAsync(string id)
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
