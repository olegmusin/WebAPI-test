using ScienceNewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json;
using System.IO;

namespace ScienceNewsAPI.Helpers
{
    public static class Validation
    {
        public static Item Validate(object item)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(Item));

            JsonTextReader reader = new JsonTextReader(new StringReader(item.ToString()));

            JSchemaValidatingReader validatingReader = new JSchemaValidatingReader(reader);
            validatingReader.Schema = schema;

            IList<string> messages = new List<string>();
            validatingReader.ValidationEventHandler += (o, a) => messages.Add(a.Message);

            JsonSerializer serializer = new JsonSerializer();
            Item i = serializer.Deserialize<Item>(validatingReader);

            if (messages.Count == 0)
                return i;
            else
                return null;
        }
    }
}
