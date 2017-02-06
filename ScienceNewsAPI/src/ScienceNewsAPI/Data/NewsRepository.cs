using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScienceNewsAPI.Data.Repository;
using ScienceNewsAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScienceNewsAPI.Data
{
    public class NewsRepository : GenericRepository<Item>
    {
        private readonly ScienceNewsDbContext _context;
        private ILogger<NewsRepository> _logger;

        public NewsRepository(ScienceNewsDbContext context, ILogger<NewsRepository> logger)
            : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<Item> FindByStringInTitle(string str) => FindBy(i => i.Title.Contains(str));
        
    }
}