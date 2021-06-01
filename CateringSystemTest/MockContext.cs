using System.Collections.Generic;

using CateringSystem;

namespace CateringSystemTest
{
    internal static class MockContext
    {
        internal static readonly IReadOnlyList<Category> _Categories;
        internal static readonly IReadOnlyList<Product> _Products;

        static MockContext()
        {
            MockContext._Categories = new List<Category>
            {
                new Category
                {
                    ID = 1,
                    Name = "Hot Food"
                },

                new Category
                {
                    ID = 2,
                    Name = "Cold Food"
                },

                new Category
                {
                    ID = 3,
                    Name = "Snacks"
                }
            };

            MockContext._Products = new List<Product>
            {
                new Product
                {
                    ID = 1,
                    Name = "Pizza",
                    Price = 3,
                    CategoryId = 1
                },
                
                new Product
                {
                    ID = 2,
                    Name = "Burger",
                    Price = 2,
                    CategoryId = 1
                },

                new Product
                {
                    ID = 3,
                    Name = "Sandwich",
                    Price = 1.50M,
                    CategoryId = 2
                },

                new Product
                {
                    ID = 4,
                    Name = "Wrap",
                    Price = 3,
                    CategoryId = 2
                },

                new Product
                {
                    ID = 5,
                    Name = "Crisps",
                    Price = 0.60M,
                    CategoryId = 3
                }
            };
        }
    }
}