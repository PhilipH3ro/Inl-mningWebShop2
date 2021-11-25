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

namespace DC2.UI.Pages.User
{
    [Authorize(Policy = "AdminOnly")]
    public class UserModel : PageModel
    {
        private readonly IDataUser UserData;

        public UserModel(IDataUser DataUser)
        {
            UserData = DataUser;
        }
        [BindProperty]
        public int Id { get; set; }
        public List<UserDTO> Users { get; set; }

        //[BindProperty]
        //public UserDTO CurrentUser { get; set; }

        public void OnGet()
        {
            Users = UserData.GetAll();
        }

        public IActionResult OnPostView()
        {
            if (!ModelState.IsValid) return Page();
            HttpContext.Session.SetInt32("UserId", Id);
            return RedirectToPage("/Cart/Index", new { id = Id });
        }
        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Edit", new { id = Id});
        }

        public IActionResult OnPostRemove()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Remove", new { id = Id });
        }
    }
}
