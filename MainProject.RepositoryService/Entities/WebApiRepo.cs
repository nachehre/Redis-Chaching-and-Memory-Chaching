using MainProject.Domain;

using MainProject.RepositoryService.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MainProject.Infra.StevenSolution
{
    public class WebApiRepo :  IRepository
    {


        private readonly IHttpClientFactory _httpClient;
        public WebApiRepo( IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<Book> GetAsync(string id)
        {
            var httpClient = _httpClient.CreateClient();

            httpClient.BaseAddress = new Uri("https://get.taaghche.com");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync("/v2/book/" + id);

            var book = new Book();
            if (response.IsSuccessStatusCode)
            {
                var obj = await response.Content.ReadAsAsync<BookModel>();


                book.Id = obj.Book.Id;
                book.Title = obj.Book.Title;
                book.Author = obj.Book.Author;
                book.PublishDate = obj.Book.PublishDate;



            }
            return book;
        }
    }
}
