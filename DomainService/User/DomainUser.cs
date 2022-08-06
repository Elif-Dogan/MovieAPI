using System.Net.Http.Headers;
using System.Text;
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
        
        public DTOUser UserGetAccountDetails(DTOUserIstek _userIstek)
        {
            try
            {
                string url = _config["ServisAdres"] + "account?api_key="+_config["api_key"]+"&session_id="+_userIstek.session_id;
                var sonuc = client.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
                sonuc.Wait();
                string donenSonuc = sonuc.Result.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOUser>(donenSonuc);
                DTOUser _user= new DTOUser();
                if(_data !=null)
                {
                    _user.id = _data.id;
                    _user.username = _data.username;
                    _user.name = _data.name;
                }
                return _user;
          }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}