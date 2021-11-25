using DC2.UI.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DC2.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly DataLogin DataLogin;

        [BindProperty]
        public Credential Credential { get; set; }
        public LoginModel()
        {
            DataLogin = new DataLogin();
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var serverAnswer = DataLogin.Validation(Credential);
            if (serverAnswer.IsValid)
            {
                var identity = new ClaimsIdentity(serverAnswer.Claims, "MyAuthCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyAuthCookie", claimsPrincipal);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
