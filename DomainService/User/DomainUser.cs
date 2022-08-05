using System.Net.Http.Headers;
using DataTransferObject.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DomainService.User
{
    public class DomainUser
    {
        public static HttpClient client = new HttpClient();
        JObject _config;
          public DomainUser()
        {
            string path = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "/API/appsettings.json");
            _config = JObject.Parse(path);
        }
        public DTOUser UserGetToken(DTOUserIstek _userIstek)
        {
            try
            {
                string url = _config["ServisAdres"] + "authentication/token/new?api_key="+_config["api_key"];
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOUser>(donenSonuc);
                return _data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}