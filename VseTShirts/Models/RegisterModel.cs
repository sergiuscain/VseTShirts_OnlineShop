using System.ComponentModel.DataAnnotations;

namespace VseTShirts.Models
{
    public class RegisterModel

    {
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(8, ErrorMessage = "Email не может быть короче 8 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(6, ErrorMessage = "Логин не может быть короче 6 символов")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(10, ErrorMessage = "ФИО не может быть короче 10 символов")]
        public string Name { get; set; }
        [MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        [MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(8, ErrorMessage = "Номер телефона не может быть короче 9 символов")]
        public string PhoneNumber { get; set; }
        public Guid Id { get; }
        public RegisterModel()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
