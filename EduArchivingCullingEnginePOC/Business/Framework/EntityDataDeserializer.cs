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
            using (FileStream fs = File.OpenRead(file))
            {
                // This will fail if the data is not returned by the data provider according to the return types defined in the public abstractions.
                return await JsonSerializer.DeserializeAsync<IList<T>>(fs, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }
}
