using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace WindowsFormsApp1.Helpers
{
    class Serialization
    {
        public void JsonSerialize(object data, string filePath)
        {
            JsonSerializer jsonSer = new JsonSerializer();
            if (File.Exists(filePath)) File.Delete(filePath);
            StreamWriter sw = new StreamWriter(filePath);

            JsonWriter jsonWriter = new JsonTextWriter(sw);

            jsonSer.Serialize(jsonWriter, data);
            jsonWriter.Close();
            sw.Close();

        }

        public object JsonDeSerialize(Type dataType, string filepath)
        {
            JObject obj = null;

            JsonSerializer jsonSer = new JsonSerializer();
            if (File.Exists(filepath)){
                StreamReader sr = new StreamReader(filepath);
                JsonReader jsonReader = new JsonTextReader(sr);
                obj = jsonSer.Deserialize(jsonReader) as JObject;
                jsonReader.Close();
                sr.Close();
            }

            return obj.ToObject(dataType);
        }
    }
}
