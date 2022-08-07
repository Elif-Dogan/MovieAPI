using System.Net.Http.Headers;
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
                string url = (string)_config["MovieServis"];
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;

          }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        
    }
}

