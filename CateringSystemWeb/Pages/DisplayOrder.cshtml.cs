using System;
using System.Collections.Generic;
using System.Linq;
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

        public PayPalOrder Order { get; private set; }

        public DisplayOrderModel(CateringContext context) => this._Context = context;

        public async Task<IActionResult> OnGetAsync([FromQuery] String token, [FromQuery] String payerId)
        {
            if ((this.Order = this._Context.GetOrder(token)) is null)
            {
                return this.NotFound();
            }

            using (PayPalClient client = new PayPalClient(PayPalOptions.Default))
            {
                await client.GetAccessTokenAsync();
                PayPalCapture capture = await client.CaptureOrderAsync(this.Order);
            }

            return this.Page();
        }
    }
}
