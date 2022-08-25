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

        private readonly ILogger<BookApiController> _logger;

        public BookApiController(ILogger<BookApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public string  Get(string bookId)
        {
            return "";
        }
    }
}
