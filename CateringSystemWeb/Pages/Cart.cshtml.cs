using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

using CateringSystem;
using CateringSystemWeb.Data;

namespace CateringSystemWeb.Pages
{
    public class CartModel : PageModel
    {
        private readonly CateringContext _Context;

        public int ItemCount { get; private set; }
        public IEnumerable<CartItem> Items { get; private set; }
        public Decimal Total { get; private set; }
        public IReadOnlyDictionary<int, Decimal> Totals { get; private set; }

        public CartModel(CateringContext context) => this._Context = context;

        public void OnGet()
        {
            Cart cart = new Cart(new CartHandler(this._Context, this.HttpContext));
            this.Items = cart.Items;
            this.Total = this.ItemCount = 0;
            Dictionary<int, Decimal> totals = new Dictionary<int, Decimal>();

            foreach (CartItem item in this.Items)
            {
                ++ this.ItemCount;
                Decimal total = item.Product.Price * item.Quantity;
                totals.Add(item.ProductId, total);
                this.Total += total;
            }

            this.Totals = totals;
        }
    }
}
