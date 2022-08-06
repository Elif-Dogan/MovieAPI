using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Login
{
    public class DTOLoginSessionIstek
    {
        [Required(ErrorMessage = "Request token giriniz!")]
        public string RequestToken { get; set; }
    }
}