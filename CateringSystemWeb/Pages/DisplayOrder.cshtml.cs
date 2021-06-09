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
    public class DisplayOrderModel : PageModel
    {
        private readonly CateringContext _Context;

        public int ItemCount { get; private set; }
        public PayPalCapture Order { get; private set; }
        public Decimal Total { get; private set; }
        public IReadOnlyDictionary<int, Decimal> Totals { get; private set; }

        public DisplayOrderModel(CateringContext context) => this._Context = context;

        public async Task<IActionResult> OnGetAsync([FromQuery] String token, [FromQuery] String payerId)
        {
            PayPalOrder order = this._Context.GetOrder(token);

            if (order is null)
            {
                return this.NotFound();
            }

            using (PayPalClient client = new PayPalClient(PayPalOptions.Default))
            {
                await client.GetAccessTokenAsync();
                this.Order = await client.CaptureOrderAsync(order);
            }

            this.Total = this.ItemCount = 0;
            Dictionary<int, Decimal> totals = new Dictionary<int, Decimal>();

            foreach (CartItem item in this.Order.Unit.Items)
            {
                ++ this.ItemCount;
                Decimal total = item.Product.Price * item.Quantity;
                totals.Add(item.ProductId, total);
                this.Total += total;
            }

            this.Totals = totals;

            return this.Page();
        }
    }
}
