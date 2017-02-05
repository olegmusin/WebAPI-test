using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScienceNewsAPI.Models;

namespace ScienceNewsAPI.Data
{
    public sealed class ScienceNewsDbContext : DbContext
    {
        private IConfigurationRoot _config;

        public ScienceNewsDbContext(DbContextOptions<ScienceNewsDbContext> options, IConfigurationRoot config) 
            : base (options)
        {
            _config = config;
            Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }        
    }
}
