using MainProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.InMemoryService.Services
{
    public interface IMemoryCachedBooksRepository
    {
        Task<Book> OnGetBook(string bookId);

        void OnSetBook(Book book);
    }
}
