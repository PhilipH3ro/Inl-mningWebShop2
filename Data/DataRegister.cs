using DC2.UI.Pages.Account;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DC2.UI.Data
{
    public class DataRegister
    {
        public ServerAnswer2 Validation2(Credential credential)
        {
            var answ = new ServerAnswer2();
            if (credential.UserName.ToLower() == "admin" && credential.Password.ToLower() == "password")
            {
                answ.SetAsAdmin2();
            }
            else
            {
                answ.SetAsInvalid2();
            }
            return answ;
        }
    }

    public class ServerAnswer2
    {
        public bool IsValid { get; set; }
        public List<Claim> Claims { get; set; }

        public void SetAsAdmin2()
        {
            IsValid = true;
            Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim("Admin", "true")
            };
        }

        public void SetAsInvalid2()
        {
            IsValid = false;
        }
    }
}
