using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Core.v3;
using ScienceNewsAPI.Data;

namespace ScienceNewsAPI.Controllers
{
    [Route("api/[controller]")]
    public class FeedController : Controller
    {
        private ILogger<FeedController> _logger;
        private NewsRepository _repo;

        public FeedController(NewsRepository repo, ILogger<FeedController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/feed/Index
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var links = _repo.GetAll();
                return Ok(await links.ToListAsync()); // 200 response
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all links: {ex.Message}");
                return new ExceptionResult(ex, true) { StatusCode = 204 };
            }
        }

        // GET api/feed/Index/title
        [HttpGet("{title}")]
        public async Task<IActionResult> Index(string title)
        {
            try
            {
                var news = await _repo.GetSingle(title);
                return Ok(news);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get link with title {title} due to: {ex.Message}");
                return new ExceptionResult(ex, true) { StatusCode = 204 };
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
