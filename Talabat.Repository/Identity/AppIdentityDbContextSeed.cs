using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task IdentitySeedAsync( UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    Name = "Yaseer Mohamed",
                    Email = "yassermohmaed@gmail.com",
                    UserName = "yassermohmaed",
                    PhoneNumber = "01202370643"
                };
                await userManager.CreateAsync(User, "Pa$$w0rd"); 
            }

        }
    }
}
