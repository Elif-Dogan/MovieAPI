using System.Net.Http.Headers;
using System.Text;
using DataTransferObject.Login;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DomainService.Login
{
    public class DomainLogin
    {
        public static HttpClient client = new HttpClient();
        JObject _config;
          public DomainLogin()
        {
            string path = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "/API/appsettings.json");
            _config = JObject.Parse(path);
        }
        public DTOLoginToken UserGetRequestToken()
        {
            try
            {
                string url = _config["ServisAdres"] + "authentication/token/new?api_key="+_config["api_key"];
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOLoginToken>(donenSonuc);
                return _data;
           }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DTOLoginToken UserGetTokenWithLogin(DTOLoginSessionWithLoginIstek _sessionIstek)
        {
            try
            {
                string url = _config["ServisAdres"] + "authentication/token/validate_with_login?api_key="+_config["api_key"];
                String jsonInString = JsonConvert.SerializeObject(_sessionIstek);
                var sonuc = client.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                sonuc.Wait();
                string donenSonuc = sonuc.Result.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOLoginToken>(donenSonuc);
                return _data;
          }
            catch (Exception ex)
            {
                return null;
            }
        }       
        public DTOLoginSession UserGetSession(DTOLoginSessionIstek _sessionIstek)
        {
            try
            {
                string url = _config["ServisAdres"] + "authentication/session/new?api_key="+_config["api_key"];
                String jsonInString = JsonConvert.SerializeObject(_sessionIstek);
                var sonuc = client.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                sonuc.Wait();
                string donenSonuc = sonuc.Result.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOLoginSession>(donenSonuc);
                return _data;
          }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}