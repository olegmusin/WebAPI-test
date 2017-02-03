using ScienceNewsAPI.Data;
using ScienceNewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceNewsAPI.Controllers
{
    public class NewsRepository
    {
        private ScienceNewsDbContext _context;

        public IEnumerable<Item> GetAll()
        {
            return _context.Items.ToList();
        }
    }
}