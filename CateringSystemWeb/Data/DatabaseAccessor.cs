using System;
using System.Linq;

using CateringSystem;
using CateringSystem.PayPal;

namespace CateringSystemWeb.Data
{
    public static class DatabaseAccessor
    {
        internal const String _SessionInitKey = "sessionInit";
        
        public static Category GetCategory(this CateringContext context, int id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof (context));
            }

            Category category = context.Categories.FirstOrDefault(c => c.ID == id);
            return category ?? throw new ArgumentException("The specified category ID could not be found.");
        }

        public static PayPalOrder GetOrder(this CateringContext context, String id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof (context));
            }

            if (id is null)
            {
                throw new ArgumentNullException(nameof (id));
            }

            PayPalOrder order = context.Orders.FirstOrDefault(p => p.ID.Equals(id));
            return order ?? throw new ArgumentException("The specified order ID could not be found.");
        }

        public static Product GetProduct(this CateringContext context, int id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof (context));
            }

            Product product = context.Products.FirstOrDefault(p => p.ID == id);
            return product ?? throw new ArgumentException("The specified product ID could not be found.");
        }
    }
}