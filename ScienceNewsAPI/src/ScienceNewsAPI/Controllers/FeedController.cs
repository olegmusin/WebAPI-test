using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Protocol.Core.v3;
using ScienceNewsAPI.Data;
using ScienceNewsAPI.Models;

namespace ScienceNewsAPI.Controllers
{
    [Route("api/feed")]
    public class FeedController : ApiController
    {
        private readonly ILogger<FeedController> _logger;
        private readonly NewsRepository _repo;

        public FeedController(NewsRepository repo, ILogger<FeedController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/feed/Search
        [HttpGet("search")]
        public async Task<IActionResult> Search()
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

        // GET api/feed/Search/title
        [HttpGet("search/{title}")]
        public async Task<IActionResult> Search(string title)
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

        // POST api/feed/Index
        [HttpPost("index")]
        public async Task<IActionResult> Index([FromBody]Item item)
        {
            //TODO: validate json schema
            if (ModelState.IsValid)
            {
                _repo.Add(item);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"New RSS item created successfully", item);
                }
            }
            _logger.LogError($"Error saving item with id {item.Id} to database");
            return BadRequest("Posting data failed!");
        }

        // PUT api/feed/index/5
        [HttpPut("index/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Item item)
        {
            if (ModelState.IsValid)
            {
               var itemUpdate = await _repo.FindBy(i => i.Id == id).SingleAsync();

                itemUpdate.Content = item.Content;
                itemUpdate.Link = item.Link;
                itemUpdate.PubDate = item.PubDate;
                itemUpdate.Thumbnail = item.Thumbnail;
                itemUpdate.Title = item.Title;
                _repo.Edit(itemUpdate);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok($"RSS item with id {itemUpdate.Id} updated successfully");
                }
            }
            _logger.LogError($"Error saving item with id {id} to database");
            return BadRequest("Putting data failed!");
        }

        // DELETE api/feed/search/5
        [HttpDelete("search/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var item = await _repo.FindBy(i => i.Id == id).SingleAsync();
                _repo.Delete(item);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok($"RSS item with id {item.Id} deleted successfully");
                }
            }
            _logger.LogError($"Error deleting item with id {id}");
            return BadRequest("Failed to delete!");
        }
    }
}
