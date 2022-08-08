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
        
        public DTOUser UserGetAccountDetails(string session_id)
        {
            try
            {
                string url = _config["ServisAdres"] + "account?api_key="+_config["api_key"]+"&session_id="+session_id;
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
        public DTORatedMovies GetRatedMovies(string session_id, int account_id)
        {
            try
            {
                string url = _config["ServisAdres"] + "account/"+account_id+"/rated/movies?api_key="+_config["api_key"]+"&session_id="+session_id;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTORatedMovies>(donenSonuc);
                return _data;
          }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}