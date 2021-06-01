using Microsoft.AspNetCore.Mvc.RazorPages;

using CateringSystemWeb.Data;

namespace CateringSystemWeb.Pages
{
    public class ProductsModel : PageModel
    {
        public CateringContext Context { get; }

        public ProductsModel(CateringContext context) => this.Context = context;
    }
}
