using System.ComponentModel.DataAnnotations;

namespace VseTShirts.Models
{
    public class DeliveryInfo
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(15, ErrorMessage = "Адресс слишком короткий")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(9, ErrorMessage = "Номер слишком короткий")]
        public string Phone { get; set; }
    }
}
