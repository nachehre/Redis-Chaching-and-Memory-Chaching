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

        public async Task<Book> OnGetBook(string bookId)
        {
            Book book = new Book();

            var result = _memoryCache.Get(bookId);

            if(result == null)
            {
                return null;
            }
            book = (Book)result;
            return book;
        }
       

        public void OnSetBook(Book book)
        {


            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));

            _memoryCache.Set(book.Id, book, cacheEntryOptions);
        }
    }
}
