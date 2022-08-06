using System.Net.Http.Headers;
using System.Text;
using DataTransferObject.Movie;
using DataTransferObject.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DomainService.Movie
{
    public class DomainMovie
    {
        public static HttpClient client = new HttpClient();
        JObject _config;
          public DomainMovie()
        {
            string path = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "/API/appsettings.json");
            _config = JObject.Parse(path);
        }
        
        public DTOMovie GetMovies(int page)
        {
            try
            {
                if(page ==0)
                    page=1;
                string url = _config["ServisAdres"] + "discover/movie?api_key="+_config["api_key"]+"&page="+page;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOMovie>(donenSonuc);
                return _data;
          }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}