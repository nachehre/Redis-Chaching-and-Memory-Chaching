using MainProject.Domain;
using System.Threading.Tasks;

namespace MainProject.RedisService.Services
{
    public interface IRedisCachedBooksRepository
    {
        Task<Book> GetBook(string bookId);

        Task<Book> SetBook(Book book);
    }
}
