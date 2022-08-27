using MainProject.Domain;
using System.Threading.Tasks;

namespace MainProject.RedisService.Services
{
    public interface IRedisCachedBooksRepository
    {
        Task<Book> GetBook(string bookId);

        void SetBook(Book book);
    }
}
