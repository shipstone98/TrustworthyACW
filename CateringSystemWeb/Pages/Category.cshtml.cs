using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using CateringSystem;
using CateringSystemWeb.Data;

namespace CateringSystemWeb.Pages
{
    public class CategoryModel : PageModel
    {
        public Category Category { get; private set; }
        public CateringContext Context { get; }

        public CategoryModel(CateringContext context) => this.Context = context;

        public IActionResult OnGet([FromQuery] int id)
        {
            try
            {
                this.Category = this.Context.GetCategory(id);
            }

            catch (ArgumentException)
            {
                return this.NotFound();
            }

            return this.Page();
        }
    }
}
