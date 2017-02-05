using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScienceNewsAPI.Models;

namespace ScienceNewsAPI.Data
{
    public class DbSeed
    {
        private readonly ScienceNewsDbContext _context;

        public DbSeed(ScienceNewsDbContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Items.Any())
            {
                using (var reader = File.OpenText(@"..\ScienceNewsAPI\Data\Data.json"))
                {
                    var o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                    IList<JToken> items = o["items"].Children().ToList();

                    foreach (var i in items)
                    {
                        Item item = JsonConvert.DeserializeObject<Item>(i.ToString());
                        _context.Items.Add(item);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}