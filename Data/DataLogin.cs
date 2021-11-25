using DC2.UI.Pages.Account;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DC2.UI.Data
{
    public class DataLogin
    {
        public ServerAnswer Validation(Credential credential)
        {
            var answ = new ServerAnswer();
            if (credential.UserName.ToLower() == "admin" && credential.Password.ToLower() == "password")
            {
                answ.SetAsAdmin();
            }
            else
            {
                answ.SetAsInvalid();
            }
            return answ;
        }
    }

    public class ServerAnswer
    {
        public bool IsValid { get; set; }
        public List<Claim> Claims { get; set; }

        public void SetAsAdmin()
        {
            IsValid = true;
            Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim("Admin", "true")
            };
        }

        public void SetAsInvalid()
        {
            IsValid = false;
        }
    }
}
