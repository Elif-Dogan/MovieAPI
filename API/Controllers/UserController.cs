using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataTransferObject.User;
using DomainService.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDistributedCache _cache;
        public UserController(IHttpContextAccessor contextAccessor, IDistributedCache cache)
        {
            _contextAccessor = contextAccessor;
            _cache = cache;
        }
        /// <summary>
        /// Kullanıcı adı ve şifre ile sisteme giriş yapmayı sağlar.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("/Login")]
        public IActionResult Login(DTOUserIstek _userIstek)
        {
            try
            {
                DomainUser _userLogin = new DomainUser();
                DTOUser _user = _userLogin.UserGetToken(_userIstek);
                
                if (_userIstek.KullaniciAdi != null && _userIstek.Sifre !=null)
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
                    _user.request_token = tokenHandler.WriteToken(token);
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
    }
}