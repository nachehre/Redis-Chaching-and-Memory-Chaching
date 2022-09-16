
using MainProject.Infra.Steven_Solution;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MainProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookApiController : ControllerBase
    {

        //decorator Pattern
        //private readonly MainProject.Infra.StevenSolution.IRepository _repository;

        //public BookApiController(IRepository repository)
        //{
        //    _repository = repository;
        //}

        private readonly IRepository _cache;

        //private readonly IRepository _redis;
        //private readonly IRepository _api;

        public BookApiController(IRepository cache)
        {
           _cache=cache;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string bookId)
        {
            if (bookId == null)

                throw new Exception("the bookId is not valid");
            //var cache = new CachedRepository(_memory, _redis, _api);
            //with chain of responsibility pattern we created a regular call for this task
            //var apiGetter = new ApiGetter(_dataRepository,_redis, _memory);

            //var redisGetter= new RedisGetter(_redis, _memory);
            //redisGetter.SetNext(apiGetter);

            //var memoryGetter = new MemoryGetter(_memory); 
            //memoryGetter.SetNext(redisGetter);
            //var book= memoryGetter.Get(bookId);
            //var book= await  _repository.GetAsync(bookId);
            var book = await _cache.GetAsync(bookId);
            return Ok(book);

        }

     
    }
}
