using System.Net.Http.Headers;
using System.Text;
using DataTransferObject.Login;
using Newtonsoft.Json;

namespace UnitTest;

[TestClass]
public class UnitTest1
{
    public static HttpClient client = new HttpClient();
    public static string address="https://localhost:7282/";
    public static string request_token="";
    [TestMethod]
    public void GetTokenTest()
    {
            try
            {
                string url = address+"RequestToken";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string donenSonuc = response.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOLoginToken>(donenSonuc);
                _data.request_token = request_token;
                if(_data !=null)
                Assert.IsTrue(true);
                else
                Assert.IsFalse(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
    }
    [TestMethod]
    public void LoginWithTokenTest()
    {
            try
            {
                DTOLoginSessionWithLoginIstek _Dto= new DTOLoginSessionWithLoginIstek();
                _Dto.username="elifdogan";
                _Dto.password="123456";
                _Dto.request_token= request_token;
                string url = address+"LoginWithToken";
                String jsonInString = JsonConvert.SerializeObject(_Dto);
                var sonuc = client.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                sonuc.Wait();
                string donenSonuc = sonuc.Result.Content.ReadAsStringAsync().Result;
                var _data = JsonConvert.DeserializeObject<DTOLoginToken>(donenSonuc);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
    }

}