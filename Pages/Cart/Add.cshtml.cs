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
using Newtonsoft.Json;

namespace DC2.UI.Pages.Cart
{
    public class AddModel : PageModel
    {
        private readonly IDataCart CartData;
        public AddModel(IDataCart DataCart)
        {
            CartData = DataCart;
        }
        [BindProperty]
        public CartDTO CurrentCart { get; set; }


        public IActionResult OnGet(int id, int productId)
        {
            int tmp = (int)HttpContext.Session.GetInt32("UserId");

            CartData.AddToCart(tmp, id);
            return RedirectToPage("./Index", new { id = tmp });
        }
    }
}
