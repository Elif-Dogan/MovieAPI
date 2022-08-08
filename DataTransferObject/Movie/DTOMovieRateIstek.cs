using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Movie
{
    public class DTOMovieRateIstek
    {
         public int MovieId { get; set; }
        public string SessionId {get; set;}
        
        [Range(1, 10, ErrorMessage="Puan 1 ile 10 arasında bir sayı olmalı!")] 
        public int Value {get; set;}
        
        [StringLength(5, ErrorMessage="Geçerli bir not girin!")] 
        public string Note {get; set;}
    }
}