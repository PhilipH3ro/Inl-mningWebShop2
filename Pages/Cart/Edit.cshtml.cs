using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Data;
using DC2.UI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DC2.UI.Pages.Cart
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly IDataCart CartData;
        public EditModel(IDataCart DataCart)
        {
            CartData = DataCart;
        }
        [BindProperty]
        public CartDTO CurrentCart { get; set; }


        public IActionResult OnGet(int id, int productId)
        {

            CurrentCart = CartData.GetById(id, productId);
            if (CurrentCart is null) return RedirectToPage("./404");

            return Page();
        }
        public IActionResult OnPostSave()
        {
            if (ModelState.IsValid)
            {
                // save the new user
                CartData.UpdateCart(CurrentCart);
                return RedirectToPage("./Index", "Edit");
            }
            return Page();
        }
    }
}
