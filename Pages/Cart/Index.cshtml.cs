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
    public class CartModel : PageModel
    {
        private readonly IDataProduct ProductData;
        private readonly IDataUser UserData;
        private readonly IDataCart CartData;
        public CartModel(IDataCart DataCart, IDataUser DataUser, IDataProduct DataProduct)
        {
            ProductData = DataProduct;
            UserData = DataUser;
            CartData = DataCart;
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int ProductId { get; set; }
        public string SearchTmp { get; set; }

        public List<UserDTO> Users { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<CartDTO> Carts { get; set; }

        public UserDTO CurrentUser { get; set; }
        public ProductDTO CurrentProduct { get; set; }
        public CartDTO CurrentCart { get; set; }


        public void OnGet(int id, int productId)
        {
            //int tmp = (int)HttpContext.Session.GetInt32("UserId");
            
            CurrentCart = CartData.GetById(id, productId);
            CurrentUser = UserData.GetById(id);
            CurrentProduct = ProductData.GetById(id);
            Carts = CartData.GetAll();
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
        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Add", new { id = Id });
        }
        public IActionResult OnPostPay()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Pay", new { id = Id });
        }
        public IActionResult OnPostRemove()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Remove", new { id = Id, productId = ProductId });
        }
        public IActionResult OnPostOrder()
        {
            var cart = new CartDTO();
            var order = new OrderDTO();
            CartData.RemoveCart(cart);

            if (!ModelState.IsValid) return Page();
            return RedirectToPage("/Order/Index", new { id = Id});
        }
    }
}