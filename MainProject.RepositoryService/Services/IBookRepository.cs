using MainProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.RepositoryService.Services
{
    public interface IBookRepository
    {
         Task<Book> GetBookById(string bookId);    

    }
}
