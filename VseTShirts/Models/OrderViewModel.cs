using System.ComponentModel.DataAnnotations;

namespace VseTShirts.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }


        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(2, ErrorMessage = "Имя слишком короткое")]
        public string Name {  get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(15, ErrorMessage = "Адресс слишком короткий")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(9, ErrorMessage = "Номер слишком короткий")]
        public string Phone { get; set; }
        public OrderStatusViewModel Status { get; set; }

        public DateTime DateAndTime { get; set; }

        public CartViewModel Cart { get; set; }
    }
}
