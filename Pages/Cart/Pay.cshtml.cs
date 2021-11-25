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
    public class PayModel : PageModel
    {
        private readonly IDataOrder OrderData;
        private readonly IDataCart CartData;
        private readonly IDataUser UserData;
        private readonly IDataProduct ProductData;


        public PayModel(IDataCart DataCart, IDataOrder DataOrder, IDataUser DataUser, IDataProduct DataProduct)
        {
            OrderData = DataOrder;
            CartData = DataCart;
            UserData = DataUser;
            ProductData = DataProduct;
        }
        [BindProperty]
        public int Id { get; set; }
        public List<OrderDTO> Orders { get; set; }
        public List<CartDTO> Carts { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<ProductDTO> Products { get; set; }

        public OrderDTO CurrentOrder { get; set; }
        public CartDTO CurrentCart { get; set; }
        public UserDTO CurrentUser { get; set; }
        public ProductDTO CurrentProduct { get; set; }


        public IActionResult OnGet(int id)
        {
            CurrentOrder = OrderData.GetById(id);
            CurrentCart = CartData.GetById(id);
            CurrentUser = UserData.GetById(id);
            CurrentProduct = ProductData.GetById(id);
            Carts = CartData.GetAll();
            Products = ProductData.GetAll();

            return Page();
            //return RedirectToPage("./Index", new { id = id });
        }
        public IActionResult OnPostPay()
        {
            if (CurrentOrder.IsPaid == 0)
            {
                return RedirectToPage("404");
            }
            else
            {
                if (!ModelState.IsValid) return Page();
                return RedirectToPage("./Pay", new { id = Id });
            }
        }

    }
}