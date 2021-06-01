using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using CateringSystem;
using CateringSystemWeb.Data;

namespace CateringSystemWeb.Pages
{
    public class AddToCartModel : PageModel
    {
        private readonly CateringContext _Context;

        public AddToCartModel(CateringContext context) => this._Context = context;

        public async Task<IActionResult> OnGetAsync([FromQuery] int productId, [FromQuery] String redir)
        {
            try
            {
                Cart cart = new Cart(new CartHandler(this._Context, this.HttpContext));
                await cart.AddAsync(productId);
            }

            catch (ArgumentException)
            {
                return this.NotFound();
            }

            return this.Redirect(redir ?? "Index");
        }
    }
}
