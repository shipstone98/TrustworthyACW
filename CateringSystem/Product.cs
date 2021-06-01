using System;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem
{
    public class Product : IEquatable<Product>
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public String Name { get; set; }

        [Required]
        [StringLength(100)]
        public String Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public Decimal Price { get; set; }

        [Required]
        public String ImageUri { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public override bool Equals(Object obj) => this.Equals(obj as Product);
        public bool Equals(Product product) => !(product is null) && this.ID.Equals(product.ID);
        public override int GetHashCode() => this.ID;
        public override String ToString() => this.Name;

        public static bool operator ==(Product a, Product b) => a is null ? b is null : a.Equals(b);
        public static bool operator !=(Product a, Product b) => !(a == b);
    }
}
