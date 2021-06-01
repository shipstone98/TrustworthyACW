using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem
{
    public class Category : IEnumerable<Product>, IEquatable<Category>
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public String Name { get; set; }

        [Required]
        [StringLength(100)]
        public String Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override bool Equals(Object obj) => this.Equals(obj as Category);
        public bool Equals(Category category) => !(category is null) && this.ID.Equals(category.ID);
        public IEnumerator<Product> GetEnumerator() => this.Products.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.Products.GetEnumerator();
        public override int GetHashCode() => this.ID;
        public override String ToString() => this.Name;

        public static bool operator ==(Category a, Category b) => a is null ? b is null : a.Equals(b);
        public static bool operator !=(Category a, Category b) => !(a == b);
    }
}
