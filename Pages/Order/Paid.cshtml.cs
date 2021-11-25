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

namespace DC2.UI.Pages.Order
{
    public class PaidModel : PageModel
    {
        private readonly IDataOrder OrderData;
        public PaidModel(IDataOrder DataOrder)
        {
            OrderData = DataOrder;
        }
        [BindProperty]
        public OrderDTO CurrentOrder { get; set; }


        public IActionResult OnGet(int id)
        {

            //OrderData.AddToCart(tmp, id);
            return RedirectToPage("./Index", new { id = id });
        }
    }
}
