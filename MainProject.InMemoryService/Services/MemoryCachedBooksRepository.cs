using MainProject.Domain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.InMemoryService.Services
{
    public class MemoryCachedBooksRepository : IMemoryCachedBooksRepository
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCachedBooksRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache; 
        }

        public Book OnGetBook(string bookId)
        {
            Book book = new Book();

            if (!_memoryCache.TryGetValue(bookId, out Book cacheValue))
            {
                cacheValue = book;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(1));

                _memoryCache.Set(bookId, cacheValue, cacheEntryOptions);
            }

            return cacheValue;
        }

     
    }
}
