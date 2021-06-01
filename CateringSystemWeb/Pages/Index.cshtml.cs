using Microsoft.AspNetCore.Mvc.RazorPages;

using CateringSystemWeb.Data;

namespace CateringSystemWeb.Pages
{
    public class IndexModel : PageModel
    {
        public CateringContext Context { get; }

        public IndexModel(CateringContext context) => this.Context = context;
    }
}
