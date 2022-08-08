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
        /// <summary>
        /// Sayfa sayfa film bilgilerini getirir.
        /// </summary>
 
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
        /// <summary>
        /// Id'ye göre film bilgilerini getirir.
        /// </summary>
        
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
         /// <summary>
        /// Filme ait olan yorumları ve yazarları getirir.
        /// </summary>
        [HttpGet("/GetMovieNotes/{movieId}")]
        public IActionResult GetMovieNotes(int movieId)
        {
            try
            {
                DomainMovie _userLogin = new DomainMovie();
                return Ok(_userLogin.GetNotesByMovieId(movieId));
                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Filme puan vermeyi sağlar.
        /// </summary>
        /// <remarks>
        /// POST /RateMovie
        ///   
        ///       MovieId: 12345   
        ///       Value: 1...10 arasında bir sayı.
        ///       Note: "abcdef" 
        ///
        /// </remarks>
        [AllowAnonymous]
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
        /// <summary>
        /// Film önerisini mail olarak gönderir.
        /// </summary>
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