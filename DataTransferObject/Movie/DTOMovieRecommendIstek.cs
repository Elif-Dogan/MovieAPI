using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Movie
{
    public class DTOMovieRecommendIstek
    {      
        [Required(ErrorMessage = "Movie Id giriniz!")]
        public int MovieId { get; set; }  
        [Required(ErrorMessage = "Email giriniz!")]
        public string Email { get; set; }
    }
}