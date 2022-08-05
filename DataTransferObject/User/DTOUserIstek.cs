using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.User
{
    public class DTOUserIstek
    {
        [Required(ErrorMessage = "Kullanıcı adı giriniz!")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "Şifre giriniz!")]
        public string Sifre { get; set; }
    }
}