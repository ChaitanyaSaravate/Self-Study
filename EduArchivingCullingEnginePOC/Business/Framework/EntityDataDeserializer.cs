using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Framework
{
    public class EntityDataDeserializer
    {
        public async Task<IList<T>> GetDeserializedData<T>(string file)
        {
            //TODO: Check why deserialization is not working.
            using (FileStream fs = File.OpenRead(file))
            {
                return await JsonSerializer.DeserializeAsync<IList<T>>(fs, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }
}
