using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Data;
using DC2.UI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DC2.UI.Pages.Product
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly IDataProduct ProductData;
        public EditModel(IDataProduct DataProduct)
        {
            ProductData = DataProduct;
        }
        [BindProperty]
        public ProductDTO CurrentProduct { get; set; }


        public IActionResult OnGet(int id)
        {
            CurrentProduct = ProductData.GetById(id);
            if (CurrentProduct is null) return RedirectToPage("./404");

            return Page();
        }
        public IActionResult OnPostSave()
        {
            if (ModelState.IsValid)
            {
                // save the new user
                ProductData.UpdateProduct(CurrentProduct);
                return RedirectToPage("./Index", "Edit");
            }
            return Page();
        }
    }
}
