using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataTransferObject.Movie;
using DomainService.Movie;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("GetMovies/{page}")]
        public IActionResult GetMovies(int page)
        {
            try
            {
                DomainMovie _account = new DomainMovie();
                return Ok(_account.GetMovies(page));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}