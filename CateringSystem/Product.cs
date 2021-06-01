using System;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(63)]
        public String Name { get; set; }

        [Required]
        [StringLength(255)]
        public String Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public Decimal Price { get; set; }

        [Required]
        public String ImageUri { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
