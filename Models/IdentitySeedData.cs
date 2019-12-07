using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Comp229_301052117_Assign3.Models
{
    public static class IdentitySeedData
    {
        private const string generalUser = "General";
        private const string generalPassword = "Secret123$";

        private const string generalUser1 = "SecondUser";
        private const string generalPassword1 = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(generalUser);
            if (user == null)
            {
                user = new IdentityUser("General");
                await userManager.CreateAsync(user, generalPassword);
            }

            IdentityUser user1 = await userManager.FindByIdAsync(generalUser1);
            if (user1 == null)
            {
                user1 = new IdentityUser("SecondUser");
                await userManager.CreateAsync(user1, generalPassword1);
            }
        }
    }
}
