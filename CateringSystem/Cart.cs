using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CateringSystem
{
    public class Cart
    {
        private readonly ICartHandler _Handler;

        public String ID { get; }
        public IEnumerable<CartItem> Items => this._Handler.Items;

        public Cart(ICartHandler handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof (handler));
            }

            if (String.IsNullOrWhiteSpace(handler.ID))
            {
                throw new NotImplementedException();
            }

            this._Handler = handler;
            this.ID = handler.ID;
        }

        public async Task AddAsync(int productId)
        {
            Product product = this._Handler.GetProduct(productId);
            CartItem cartItem = this.Items.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem is null)
            {
                cartItem = new CartItem
                {
                    CartId = this.ID,
                    Product = product,
                    Quantity = 1
                };

                await this._Handler.AddCartItemAsync(cartItem);
            }

            else
            {
                ++ cartItem.Quantity;
            }

            await this._Handler.SaveChangesAsync();
        }
    }
}