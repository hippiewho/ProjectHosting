using Newtonsoft.Json.Linq;
using System.Collections;

namespace myWebsite.Models
{
    public class JsonObject
    {
        public string Author = "";
        public ArrayList commits = new ArrayList();
        private object RawJson = "";

        public JsonObject(string jsonString)
        {
            this.RawJson = jsonString;

            parseJson(jsonString);
        }

        private void parseJson(string jsonString)
        {
            System.Diagnostics.Debug.WriteLine(jsonString);
            JArray json = JArray.Parse(jsonString);
            System.Diagnostics.Debug.WriteLine(json);
        }
    }
}