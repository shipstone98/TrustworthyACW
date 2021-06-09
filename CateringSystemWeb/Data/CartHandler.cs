using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using CateringSystem;

namespace CateringSystemWeb.Data
{
    internal class CartHandler : ICartHandler
    {
        private readonly CateringContext _DbContext;
        private readonly HttpContext _HttpContext;

        public String ID { get; }
        public IEnumerable<CartItem> Items => this._DbContext.CartItems.ToList().Where(item => item.CartId.Equals(this.ID));

        public CartHandler(CateringContext dbContext, HttpContext httpContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof (dbContext));
            }

            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof (httpContext));
            }

            this._DbContext = dbContext;
            this._HttpContext = httpContext;
            this.ID = httpContext.Session.Id;
            Nullable<int> sessionInit = httpContext.Session.GetInt32(DatabaseAccessor._SessionInitKey);

            if (!sessionInit.HasValue || sessionInit.Value == 0)
            {
                httpContext.Session.SetInt32(DatabaseAccessor._SessionInitKey, 1);
            }
        }

        public async Task AddCartItemAsync(CartItem cartItem) => await this._DbContext.AddAsync(cartItem);
        public Product GetProduct(int productId) => this._DbContext.GetProduct(productId);
        public async Task SaveChangesAsync() => await this._DbContext.SaveChangesAsync();
    }
}