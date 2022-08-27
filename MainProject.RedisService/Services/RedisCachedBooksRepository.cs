using MainProject.Domain;
using Microsoft.Extensions.Caching.Distributed;
using System;
using ServiceStack.Redis;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainProject.RedisService.Services
{
    public class RedisCachedBooksRepository : IRedisCachedBooksRepository
    {

       
        private readonly IRedisClient _redisClient;

        public RedisCachedBooksRepository(IRedisClient redisClient)
        {
            
            _redisClient = redisClient;

        }

        public async Task<Book> GetBook(string bookId)
        {

            var result = new Book();
            result = _redisClient.Get<Book>(bookId);

            return result;

        }

        public void SetBook(Book book)
        {
            var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(book));
            _redisClient.Set("Book_" + book.Id, content, TimeSpan.FromMinutes(5));

        }
    }
}
