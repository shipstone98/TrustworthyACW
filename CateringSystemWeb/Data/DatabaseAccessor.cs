using System;
using System.Linq;

using CateringSystem;

namespace CateringSystemWeb.Data
{
    public static class DatabaseAccessor
    {
        public static Category GetCategory(this CateringContext context, int id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof (context));
            }

            Category category = context.Categories.FirstOrDefault(c => c.ID == id);
            return category ?? throw new ArgumentException("The specified category ID could not be found.");
        }
    }
}