using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string OrderNumber { get; set; } = 
            new Random((int)DateTimeOffset.Now.ToUnixTimeMilliseconds())
                .Next(100000, 999999)
                .ToString();
        [Precision(18,2)]
        public decimal OrderTotal { get; set; }
        [MaxLength(100)]
        public string OrderStatus { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = 
            new List<OrderItem>();
    }
}