using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Data;
using DC2.UI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DC2.UI.Pages.User
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly IDataUser UserData;
        public EditModel(IDataUser DataUser)
        {
            UserData = DataUser;
        }

        [BindProperty]
        public UserDTO CurrentUser { get; set; }

        public IActionResult OnGet(int id)
        {
            CurrentUser = UserData.GetById(id);
            if (CurrentUser is null) return RedirectToPage("./404");

            return Page();
        }
        public IActionResult OnPostSave()
        {
            if (ModelState.IsValid)
            {
                // save the new user
                UserData.UpdateUser(CurrentUser);
                return RedirectToPage("./Index", "Edit");
            }
            return Page();
        }
    }
}
