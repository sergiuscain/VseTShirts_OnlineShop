using System.ComponentModel.DataAnnotations;

namespace VseTShirts.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} ?????????? ?? {2} ?? ?? ?? {1} ???", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
    }
}
