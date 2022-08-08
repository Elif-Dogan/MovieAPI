using System.Net.Http.Headers;
using DataTransferObject.Movie;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DomainService
{
    public class MovieServis
    {
        public static HttpClient client = new HttpClient();
        JObject _config;
        public MovieServis()
        {
            string path = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "/API/appsettings.json");
            _config = JObject.Parse(path);
        }
        
        public void GetAllMovies()
        {
            try
            {
                string url = _config["ServisAdres"] + "discover/movie?api_key="+_config["api_key"];
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                DTOMovie _data = JsonConvert.DeserializeObject<DTOMovie>(donenSonuc);
                List<Result> _res= _data.results.Take(10).ToList();

          }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        
    }
}

