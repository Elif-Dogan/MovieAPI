using System.Net;
using System.Net.Http.Headers;
using System.Text;
using DataTransferObject.Movie;
using MimeKit;
using MimeKit.Text;
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
        public DTOMovieNote GetNotesByMovieId(int movieId)
        {
            try
            {
                string url = _config["ServisAdres"] + "movie/"+movieId+"/reviews?api_key="+_config["api_key"];
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOMovieNote>(donenSonuc);
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
                DTOMovieRateIstekValue _dtoValue = new DTOMovieRateIstekValue();
                _dtoValue.value= Convert.ToDouble(_movieIstek.Value);
                string url = _config["ServisAdres"] + "movie/"+_movieIstek.MovieId+"/rating?api_key="+_config["api_key"]+"&session_id="+_movieIstek.SessionId;
                client.DefaultRequestHeaders.Accept.Clear();
                String jsonInString = JsonConvert.SerializeObject(_dtoValue);
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
                DTOMovieById _movie = GetMovieById(_userIstek.MovieId);  
                string _movieLink= (string)_config["MovieLink"]+_movie.id;
                if(_movie !=null)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var mailMessage = new MimeMessage();
                    mailMessage.From.Add(new MailboxAddress("Recommender",(string)_config["From"]));
                    mailMessage.To.Add(new MailboxAddress("Movie",_userIstek.Email));
                    mailMessage.Subject = "New Recommend: ***"+_movie.original_title+"***";
                    mailMessage.Body = new TextPart(TextFormat.Plain)
                    {
                         Text = "Hi! I recommend you this film:\n"+"Title:"
                                +_movie.original_title+"\nVote Average:"+_movie.vote_average
                                +"\nOverview:"+_movie.overview+"\nLink:"+_movieLink
                    };

                    using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
                    {
                        smtpClient.Connect((string)_config["SmtpServer"],(int)_config["Port"],false);                   
                        smtpClient.Authenticate((string)_config["From"],(string)_config["Password"]);                   
                        smtpClient.Send(mailMessage);
                        smtpClient.Disconnect(true);

                    }
                    DTOMovieRecommend _dtoMovie= new DTOMovieRecommend();
                    _dtoMovie.status=true;
                    _dtoMovie.Message="Mail has been sent.";
                    return _dtoMovie;
                }
                //else
                return null;
          }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}