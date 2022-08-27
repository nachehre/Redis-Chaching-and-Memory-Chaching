using MainProject.Domain;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainProject.RedisService.Services
{
    public class RedisCachedBooksRepository :IRedisCachedBooksRepository
    {
      
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;
       
        public RedisCachedBooksRepository(IDistributedCache cache, IConnectionMultiplexer redis)
        {
            _cache = cache;
            _redis = redis;
                       
        }

        public async Task<Book> GetBook(string bookId)
        {
           

            var result = new Book();
            result = JsonSerializer.Deserialize<Book>(await _cache.GetStringAsync(bookId));


            return result;

        }

        public async void SetBook(Book book)
        {
            var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(book));

            await _cache.SetAsync("Book_" + book.Id, content, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) });
            

            
        }
    }
}
