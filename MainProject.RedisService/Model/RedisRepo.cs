using MainProject.Domain;
using ServiceStack.Redis;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainProject.Infra.StevenSolution
{
    public class RedisRepo : IRepository
    {

        private readonly IRepository _decorateRepo;
        private readonly IRedisClient _redisClient;

        public RedisRepo(IRepository decorateRepo, IRedisClient redisClient)
        {
            _decorateRepo = decorateRepo;
            _redisClient = redisClient;
        }

        public async Task<Book> GetAsync(string id)
        {

            if (false)
            {

                var result = new Book();
                result =  _redisClient.Get<Book>(id);

                return result;
            }
            else
            {

             return await  _decorateRepo.GetAsync(id);
            }
        }

        public void SetBook(Book book)
        {
            var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(book));
            _redisClient.Set("Book_" + book.Id, content, TimeSpan.FromMinutes(5));

        }


    }
}
