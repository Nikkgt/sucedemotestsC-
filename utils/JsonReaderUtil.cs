using Newtonsoft.Json.Linq;

namespace saucedemotests.utils
{
    public class JsonReaderUtil
    {
        public JToken JToken{private set; get;}
        public string JContent {private set; get;}
        public JsonReaderUtil(string jsonRelativePath)
        {
            ReadJson(jsonRelativePath);
        }

        private void ReadJson(string jsonRelativePath){
            JContent = File.ReadAllText(jsonRelativePath);
            JToken = JToken.Parse(JContent);
        }

        public string GetValue(string key){
            return JToken.SelectToken(key).Value<string>();
        }

        public IEnumerable<string> GetValues(string key){
            return JToken.SelectToken(key).Values<string>();
        }
    }
}