using System.Net.Http.Headers;
using System.Text;
using DataTransferObject.Login;
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
        public DTOLoginToken UserGetRequestToken(DTOLoginTokenIstek _userIstek)
        {
            try
            {
                string url = _config["ServisAdres"] + "authentication/token/new?api_key="+_config["api_key"];
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOLoginToken>(donenSonuc);

                if(_data.success)
                {
                    return _data;
                }
                else
                {
                    return null;
                }
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