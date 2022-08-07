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
        
        [HttpGet("GetMovieById/{movieId}")]
        public IActionResult GetMovieById(int movieId)
        {
            try
            {
                DomainMovie _account = new DomainMovie();
                return Ok(_account.GetMovieById(movieId));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/RateMovie")]
        public IActionResult RateMovie(DTOMovieRateIstek _userIstek)
        {
            try
            {
                DomainMovie _userLogin = new DomainMovie();
                return Ok(_userLogin.RateMovie(_userIstek));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
[AllowAnonymous]
        [HttpPost("/RecommendMovie")]
        public IActionResult RecommendMovie(DTOMovieRecommendIstek _userIstek)
        {
            try
            {
                DomainMovie _userLogin = new DomainMovie();
                return Ok(_userLogin.MovieRecommend(_userIstek));
                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}