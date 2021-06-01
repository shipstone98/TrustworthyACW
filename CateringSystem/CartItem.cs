using System;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem
{
    public class CartItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public String CartId { get; set; }

        public virtual Product Product { get; set; }
    }
}