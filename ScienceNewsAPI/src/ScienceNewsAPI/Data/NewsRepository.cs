using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.v3;
using ScienceNewsAPI.Data.Repository;
using ScienceNewsAPI.Models;

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

        public async Task<Item> GetSingle(string title)
         => await GetAll()
                    .FirstOrDefaultAsync(i => i.Title.Contains(title));

        public override IQueryable<Item> GetAll()
         => base.GetAll()
                    .Include(i => i.Thumbnail);

        public override IQueryable<Item> FindBy(Expression<Func<Item, bool>> predicate)
         => _context.Set<Item>()
                    .Include(i => i.Thumbnail)
                    .Where(predicate);
    }
}