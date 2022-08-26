using MainProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.RepositoryService.Services
{
    public class BookRepositoory : IBookRepository
    {
        //private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClient;

      

        public BookRepositoory(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Book> GetBookById(string bookId)
        {
            var httpClient = _httpClient.CreateClient();

            httpClient.BaseAddress = new Uri("https://get.taaghche.com");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             HttpResponseMessage response = await httpClient.GetAsync("/v2/book/"+bookId);

            var book = new Book();
            if (response.IsSuccessStatusCode)
            {
                var obj = await response.Content.ReadAsAsync<BookModel>();

             
                book.Id = obj.Book.Id;
                book.Title = obj.Book.Title;
                book.Author = obj.Book.Author;
                book.PublishDate = obj.Book.PublishDate;
                


            }
            return book ;
        }
    }
}
