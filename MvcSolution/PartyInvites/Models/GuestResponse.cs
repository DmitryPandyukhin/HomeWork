using System.ComponentModel.DataAnnotations;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите корректный email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите номер телефона")]
        public string Phone { get; set; }
        // Если бы тип bool не был nullable, то проверка Required 
        // не сработала
        [Required(ErrorMessage = "Подтвердите участие")]
        public bool? WillAttend { get; set; }
    }
}
