using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Login
{
    public class DTOLoginSessionIstek
    {
        [Required(ErrorMessage = "Request token giriniz!")]
        public string request_token { get; set; }
    }
}