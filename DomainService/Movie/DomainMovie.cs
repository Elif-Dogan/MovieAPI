using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
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
        public DTOMovieById GetMovieById(int movieId)
        {
            try
            {
                string url = _config["ServisAdres"] + "movie/"+movieId+"?api_key="+_config["api_key"];
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOMovieById>(donenSonuc);
                return _data;
          }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DTOMovieRate RateMovie(DTOMovieRateIstek _movieIstek)
        {
            try
            {
                string url = _config["ServisAdres"] + "movie/"+_movieIstek.MovieId+"/rating?api_key="+_config["api_key"]+"&session_id="+_movieIstek.SessionId;
                client.DefaultRequestHeaders.Accept.Clear();
                String jsonInString = JsonConvert.SerializeObject(_movieIstek.Value);
                var sonuc = client.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                sonuc.Wait();
                string donenSonuc = sonuc.Result.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOMovieRate>(donenSonuc);
                return _data;
           }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DTOMovieRecommend MovieRecommend(DTOMovieRecommendIstek _userIstek)
        {
            try
            {
                // DTOMovieById _movie = GetMovieById(_userIstek.MovieId);
                // if(_movie !=null)
                // {
                //     var smtpClient = new SmtpClient("smtp.gmail.com")
                //     {
                //         Port = 587,
                //         Credentials = new NetworkCredential("", "password"),
                //         EnableSsl = true,
                //     };
    
                //     smtpClient.Send("email", "recipient", "subject", "body");
                // }
                return null;
          }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}