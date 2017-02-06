using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Packaging;
using ScienceNewsAPI.Data;
using ScienceNewsAPI.Models;
using ScienceNewsAPI.Helpers;

namespace ScienceNewsAPI.Controllers
{
    [Route("api")]
    public class FeedController : ApiController
    {
        private readonly ILogger<FeedController> _logger;
        private readonly NewsRepository _repo;

        public FeedController(NewsRepository repo, ILogger<FeedController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/Search
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

        // GET api/Search/{stringToSearch}
        [HttpGet("search/{str}")]
        public async Task<IActionResult> Search(string str)
        {
            try
            {
                var news = await _repo.FindByStringInTitle(str).ToListAsync();
                return Ok(news);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get link with title contains {str} due to: {ex.Message}");
                return new ExceptionResult(ex, true) { StatusCode = 204 };
            }
        }

        // POST api/Index
        [HttpPost("index")]
        public async Task<IActionResult> Index([FromBody]object obj)
        {
            if (ModelState.IsValid)
            {
                var item = Validation.Validate(obj);
                if (item != null)
                {
                    _repo.Add(item);
                    if (await _repo.SaveChangesAsync())
                        return Created($"New RSS item created successfully", item);
                }
            }
            _logger.LogError($"Error saving item to database");
            return BadRequest("Posting data failed!");
        }
        // POST api/Index/addWithList
        [HttpPost("index/addWithList")]
        public async Task<IActionResult> Index([FromBody]object[] array)
        {
            if (ModelState.IsValid)
            {
                var createdItems = new List<Item>();
                foreach (var obj in array)
                {
                    var item = Validation.Validate(obj);
                    if (item == null) continue;
                    _repo.Add(item);
                    createdItems.Add(item);
                }

                if (await _repo.SaveChangesAsync())
                    return Created($"{createdItems.Count} of {array.Length} items created successfully", createdItems);
            }
            _logger.LogError($"Error saving items to database");
            return BadRequest("Posting list of data failed!");
        }

        // PUT api/index/5
        [HttpPut("index/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Item item)
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
                    return Ok($"RSS item with id {itemUpdate.Id} updated successfully");
            }
            _logger.LogError($"Error saving item with id {id} to database");
            return BadRequest("Putting data failed!");
        }

        // DELETE api/search/5
        [HttpDelete("search/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _repo.FindBy(i => i.Id == id).SingleAsync();
                    _repo.Delete(item);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error deleting item with id {id}: " + ex.Message);
                    return BadRequest("Failed to delete!");
                }

                if (await _repo.SaveChangesAsync())
                    return Ok($"RSS item with id {id} deleted successfully");
            }
            _logger.LogError($"Error deleting item with id {id}");
            return BadRequest("Failed to delete!");
        }
        // DELETE api/search/deleteWithList
        [HttpDelete("search/deleteWithList")]
        public async Task<IActionResult> Delete([FromBody]int[] ids)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var id in ids)
                    {
                        var item = await _repo.FindBy(i => i.Id == id).SingleAsync();
                        _repo.Delete(item);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error deleting items: " + ex.Message);
                    return BadRequest("Failed to delete!");
                }
                if (await _repo.SaveChangesAsync())
                {
                    return Ok($"RSS items list deleted successfully");
                }
            }
            _logger.LogError($"Error deleting items");
            return BadRequest("Failed to delete!");
        }
    }
}
