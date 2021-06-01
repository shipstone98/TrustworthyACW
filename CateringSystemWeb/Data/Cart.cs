using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using CateringSystem;

namespace CateringSystemWeb.Data
{
    public class Cart
    {
        private readonly CateringContext _Context;
        private readonly String _ID;

        public IEnumerable<CartItem> Items => this._Context.CartItems.Where(item => item.CartId.Equals(this._ID));

        public Cart(CateringContext dbContext, HttpContext httpContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof (dbContext));
            }

            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof (httpContext));
            }

            this._Context = dbContext;
            this._ID = httpContext.Session.Id;
            Nullable<int> sessionInit = httpContext.Session.GetInt32(DatabaseAccessor._SessionInitKey);

            if (!sessionInit.HasValue || sessionInit.Value == 0)
            {
                httpContext.Session.SetInt32(DatabaseAccessor._SessionInitKey, 1);
            }
        }

        public async Task AddAsync(int productId)
        {
            Product product = this._Context.GetProduct(productId);
            CartItem cartItem = this.Items.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem is null)
            {
                cartItem = new CartItem
                {
                    CartId = this._ID,
                    Product = product,
                    Quantity = 1
                };

                await this._Context.AddAsync(cartItem);
            }

            else
            {
                ++ cartItem.Quantity;
            }

            await this._Context.SaveChangesAsync();
        }
    }
}