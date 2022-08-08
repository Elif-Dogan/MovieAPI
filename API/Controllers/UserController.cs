using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DomainService.User;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Kullanıcı account detaylarını getirir.
        /// </summary>
 
        [HttpGet("GetAccountDetails/{session_id}")]
        public IActionResult GetAccountDetails(string session_id)
        {
            try
            {
                DomainUser _account = new DomainUser();
                return Ok(_account.UserGetAccountDetails(session_id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Kullanıcının puan verdiği filmleri getirir.
        /// </summary>
        [HttpGet("GetRatedMovies/{session_id}/{account_id}")]
        public IActionResult GetRatedMovies(string session_id, int account_id)
        {
            try
            {
                DomainUser _account = new DomainUser();
                return Ok(_account.GetRatedMovies(session_id, account_id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}