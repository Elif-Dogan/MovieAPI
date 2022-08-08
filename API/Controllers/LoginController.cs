using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataTransferObject.Login;
using DomainService.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDistributedCache _cache;
        public LoginController(IHttpContextAccessor contextAccessor, IDistributedCache cache)
        {
            _contextAccessor = contextAccessor;
            _cache = cache;
        }
        
        [AllowAnonymous]
        [HttpGet("/RequestToken")]
        public IActionResult RequestToken()
        {
            try
            {
                DomainLogin _userLogin = new DomainLogin();
                return Ok(_userLogin.UserGetRequestToken());
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        
        /// <summary>
        /// Kullanıcı adı, şifre ve request token ile sisteme giriş yapıp geriye yeni bir request token döndürür.
        /// Dönen bearer token ile de apiye giriş sağlanır.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("/LoginWithToken")]
        public IActionResult LoginWithToken( DTOLoginSessionWithLoginIstek _userIstek)
        {
            try
            {
                DomainLogin _userLogin = new DomainLogin();
                DTOLoginToken _user = _userLogin.UserGetTokenWithLogin(_userIstek);
                
                if (_userIstek.username != null && _userIstek.password !=null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("2qJdv*TZKqgAMs@ow7n39a%9tZ?5eWA^d0Z");
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(_user)));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        NotBefore = DateTime.UtcNow,
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    _user.BearerToken = tokenHandler.WriteToken(token);
                    return Ok(_user);
                }
                else
                    return Unauthorized("Kullanıcı Bilgileriniz Hatalı. Lütfen kontrol edip tekrar deneyin");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// LoginWithToken'dan dönen request_token ile işlem yapılır.Geriye session_id döndürür ve themoviedb'ye erişim sağlanmış olur. 
        /// </summary>
        [HttpPost("/GetSession")]
        public IActionResult GetSession(DTOLoginSessionIstek _userIstek)
        {
            try
            {
                DomainLogin _userLogin = new DomainLogin();
                return Ok(_userLogin.UserGetSession(_userIstek));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}