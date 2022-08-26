using MainProject.Domain;
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


            //Step1: Check the memory for given bookId
            var result = await _memory.OnGetBook(bookId);

            if (result == null)
            {

                result = await CheckRedisValue(bookId);


            }

            return Ok(result);

        }

        private async Task<Book> CheckRedisValue(string bookId)
        {
            var result = await _redis.GetBook(bookId);
            if (result != null)
            {
                Book book = new Book
                {
                    Id = result.Id,
                    Title = result.Title,
                    Author = result.Author,
                    PublishDate = result.PublishDate

                };
                _memory.OnSetBook(book);
                return result;

            }
            else
            {
                return await CheckApiRepository(bookId);

            }
        }

        private async Task<Book> CheckApiRepository(string bookId)
        {
            var result = await _dataRepository.GetBookById(bookId);
            Book book = new Book
            {
                Id = result.Id,
                Title = result.Title,
                Author = result.Author,
                PublishDate = result.PublishDate

            };
            _memory.OnSetBook(book);
            _redis.SetBook(book);
            return result;
        }
    }
}
