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
    public class RemoveModel : PageModel
    {

        [BindProperty]
        public UserDTO CurrentUser { get; set; }
        private readonly IDataUser UserData;

        public RemoveModel(IDataUser DataUser)
        {
            UserData = DataUser;
        }
        public IActionResult OnGet(int id)
        {

            CurrentUser = UserData.GetById(id);

            UserData.RemoveUser(CurrentUser);
            return RedirectToPage("./Index");
        }

    }
}
