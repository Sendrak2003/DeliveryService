using DeliveryService.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.DTO
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Range(1, double.MaxValue, ErrorMessage = "Вес должен быть положительным")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [StringLength(255)]
        public string District { get; set; } = string.Empty;

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [FutureDate(ErrorMessage = "Дата доставки должна быть больше текущей даты")]
        public DateTime DeliveryTime { get; set; }
    }
}
