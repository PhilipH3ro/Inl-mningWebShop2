using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Data;
using DC2.UI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DC2.UI.Pages.Product
{
    public class AddProductModel : PageModel
    {
        private readonly IDataProduct ProductData;
        public AddProductModel(IDataProduct DataProduct)
        {
            ProductData = DataProduct;
        }
        [BindProperty]
        public ProductDTO CurrentProduct { get; set; }


        public void OnGet()
        {

        }

        public IActionResult OnPostSave()
        {
            if (!ModelState.IsValid) return Page();
            ProductData.AddProduct(CurrentProduct);
            return RedirectToPage("./Index");
        }
    }
}
