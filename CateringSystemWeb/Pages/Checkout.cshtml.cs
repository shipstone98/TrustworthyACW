using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using CateringSystem;
using CateringSystem.PayPal;
using CateringSystemWeb.Data;

namespace CateringSystemWeb.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly CateringContext _Context;

        public CheckoutModel(CateringContext context) => this._Context = context;

        public async Task<IActionResult> OnPostAsync([FromQuery] String cartId)
        {
            Cart cart = new Cart(new CartHandler(this._Context, this.HttpContext));
            ICollection<CartItem> cartItems = new List<CartItem>(cart.Items);

            if (cartItems.Count == 0)
            {
                return this.Redirect("Index");
            }

            using (PayPalClient client = new PayPalClient(PayPalOptions.Default))
            {
                await client.GetAccessTokenAsync();
                PayPalOrder order = await client.OrderAsync(cartItems);
                await this._Context.Orders.AddAsync(order);
                await this._Context.SaveChangesAsync();
                return this.Redirect(order.ApproveLink);
            }
        }
    }
}
