using MainProject.Domain;
using MainProject.Infra;
using MainProject.InMemoryService.Services;
using MainProject.RedisService.Services;
using MainProject.RepositoryService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookApiController : ControllerBase
    {

       
        private readonly IBookRepository _dataRepository;
        private readonly IRedisCachedBooksRepository _redis;
        private readonly IMemoryCachedBooksRepository _memory;

        public BookApiController(
            IBookRepository dataRepository,
            IRedisCachedBooksRepository redis,
            IMemoryCachedBooksRepository memory
            )
        {
            
            _memory = memory;
            _redis = redis;
            _dataRepository = dataRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string bookId)
        {
            if (bookId == null)

                throw new Exception("the bookId is not valid");

            //with chain of responsibility pattern we created a regular call for this task
            var apiGetter = new ApiGetter(_dataRepository,_redis, _memory);

            var redisGetter= new RedisGetter(_redis, _memory);
            redisGetter.SetNext(apiGetter);

            var memoryGetter = new MemoryGetter(_memory); 
            memoryGetter.SetNext(redisGetter);
            var book= memoryGetter.Get(bookId);
         
            return Ok(book);

        }

     
    }
}
