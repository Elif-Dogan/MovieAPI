using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Login
{
    public class DTOLoginTokenIstek
    {
        [Required(ErrorMessage = "Kullanıcı adı giriniz!")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "Şifre giriniz!")]
        public string Sifre { get; set; }
    }
}