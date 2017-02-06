using ScienceNewsAPI.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using System.IO;
using NuGet.Protocol.Core.v3;

namespace ScienceNewsAPI.Helpers
{
    public static class Validation
    {
        public static Item Validate(object item)
        {
            var schema = JSchema.Parse(File.ReadAllText(@"..\ScienceNewsAPI\Data\JSchema.json"));
            var reader = new JsonTextReader(new StringReader(item.ToJson()));
            var validatingReader = new JSchemaValidatingReader(reader) {Schema = schema};

            IList<string> messages = new List<string>();
            validatingReader.ValidationEventHandler += (o, e) => messages.Add(e.Message);

            var serializer = new JsonSerializer();
            var i = serializer.Deserialize<Item>(validatingReader);

            return messages.Count == 0 ? i : null;
        }
    }
}
