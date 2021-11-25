using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Data;
using DC2.UI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DC2.UI.Pages.Cart
{
    [Authorize(Policy = "AdminOnly")]
    public class RemoveModel : PageModel
    {
        private readonly IDataCart CartData;
        public RemoveModel(IDataCart DataCart)
        {
            CartData = DataCart;
        }
        [BindProperty]
        public CartDTO CurrentCart { get; set; }


        public IActionResult OnGet(int id, int productId)
        {
            

            CurrentCart = CartData.GetById(id, productId);

            CartData.RemoveCart(CurrentCart);
            return RedirectToPage("./Index", new { id = id });
        }

    }
}
