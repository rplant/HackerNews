using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackNews.Interfaces;
using HackNews.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackNewsController : ControllerBase
    {

        IHackNewsBusiness _hackNewsBusiness;

        public HackNewsController(IHackNewsBusiness hackNewsBusiness)
        {
            _hackNewsBusiness = hackNewsBusiness;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(List<INewsStory>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string searchTerm)
        {
            try
            {
                return Ok(await _hackNewsBusiness.GetNewHackStories(searchTerm));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Log Error {ex.Message} to fake Log file (console in this case)");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        
        }



    }
}
