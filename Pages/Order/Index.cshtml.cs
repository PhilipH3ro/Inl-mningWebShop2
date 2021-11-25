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

namespace DC2.UI.Pages.Order
{
    [Authorize(Policy = "AdminOnly")]
    public class OrderModel : PageModel
    {
        private readonly IDataProduct ProductData;
        private readonly IDataUser UserData;
        private readonly IDataCart CartData;
        private readonly IDataOrder OrderData;
        public OrderModel(IDataOrder DataOrder, IDataCart DataCart, IDataUser DataUser, IDataProduct DataProduct)
        {
            ProductData = DataProduct;
            UserData = DataUser;
            CartData = DataCart;
            OrderData = DataOrder;
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int ProductId { get; set; }

        public List<ProductDTO> Products { get; set; }
        public List<CartDTO> Carts { get; set; }
        public List<OrderDTO> Orders { get; set; }

        public UserDTO CurrentUser { get; set; }
        public ProductDTO CurrentProduct { get; set; }
        public CartDTO CurrentCart { get; set; }
        public OrderDTO CurrentOrder { get; set; }


        public void OnGet(int id, int productId)
        {
            CurrentOrder = OrderData.GetById(id);
            CurrentCart = CartData.GetById(id, productId);
            CurrentUser = UserData.GetById(id);
            CurrentProduct = ProductData.GetById(id);
            Carts = CartData.GetAll();
            Products = ProductData.GetAll();
            Orders = OrderData.GetAll();
        }

        public IActionResult OnPostPay()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Paid", new { id = Id });
        }
        public IActionResult OnPostRemove()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Remove", new { id = Id});
        }
        public IActionResult OnPostOrder()
        {



            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Index", new { id = Id});
        }
    }
}