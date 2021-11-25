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
    public class RemoveModel : PageModel
    {
        private readonly IDataProduct ProductData;
        public RemoveModel(IDataProduct DataProduct)
        {
            ProductData = DataProduct;
        }
        [BindProperty]
        public ProductDTO CurrentProduct { get; set; }


        public IActionResult OnGet(int id)
        {
            CurrentProduct = ProductData.GetById(id);

            ProductData.RemoveProduct(CurrentProduct);
            return RedirectToPage("./Index");
        }

    }
}
