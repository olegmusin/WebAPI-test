using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScienceNewsAPI.Models;

namespace ScienceNewsAPI.Data
{
    public class ScienceNewsDbContext : DbContext
    {
        private IConfigurationRoot config;

        public ScienceNewsDbContext(DbContextOptions<ScienceNewsDbContext> options, IConfigurationRoot config) 
            : base (options)
        {
            this.config = config;
        }

        public DbSet<Item> Items { get; set; }        
    }
}
