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
    public class ProductModel : PageModel
    {
        private readonly IDataProduct ProductData;
        public ProductModel(IDataProduct DataProduct)
        {
            ProductData = DataProduct;
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string SearchTmp { get; set; }

        public List<ProductDTO> Products { get; private set; }


        public void OnGet(int id)
        {
            Products = ProductData.GetAll();
        }
        public ActionResult OnPostSearch()
        {
            if (!string.IsNullOrEmpty(SearchTmp))
            {
                Products = ProductData.GetAll().Where(x => x.Name.ToLower().Contains(SearchTmp.ToLower())).ToList();
                return Page();
            }
            return RedirectToPage("./Index");
        }
        public IActionResult OnPostSortPriceAsc()
        {
            Products = ProductData.GetAll().ToList().OrderBy(x => x.Price).ToList();
            return Page();
        }
        public IActionResult OnPostSortPriceDes()
        {
            Products = ProductData.GetAll().ToList().OrderByDescending(x => x.Price).ToList();
            return Page();
        }
        public IActionResult OnPostCategory()
        {
            //Products = ProductData.GetAll().ToList().OrderByDescending(x => x.Price).ToList();
            return Page();
        }

        public IActionResult OnPostBuy()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./AddToCart", new { id = Id });
        }
        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Edit", new { id = Id });
        }

        public IActionResult OnPostRemove()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Remove", new { id = Id });
        }
    }
}
