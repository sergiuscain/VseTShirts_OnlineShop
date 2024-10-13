

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VseTShirts.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(6, ErrorMessage = "Логие не может быть короче 6 символов")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов")]
        [PasswordPropertyText]
        public string Password { get; set; }
        public bool isRemembMe { get; set; }
    }
}
