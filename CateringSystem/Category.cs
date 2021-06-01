using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(63)]
        public String Name { get; set; }

        [Required]
        [StringLength(255)]
        public String Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
