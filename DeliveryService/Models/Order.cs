using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DeliveryService.Validation;

namespace DeliveryService.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Вес должен быть положительным")]
        [Column("Weight")]
        public double? Weight { get; set; } = 0;

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("District", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? District { get; set; } = string.Empty;

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("DeliveryTime", TypeName = "datetime")]
        [FutureDate(ErrorMessage = "Дата доставки должна быть больше текущей даты")]
        public DateTime? DeliveryTime { get; set; }
    }
}
