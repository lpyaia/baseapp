using BaseApp.Infrastructure.Identity.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BaseApp.Infrastructure.Identity.Authentication
{
    public class IdentityDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityDbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            if (!await _roleManager.RoleExistsAsync(Role.Admin))
                await _roleManager.CreateAsync(new IdentityRole(Role.Admin));

            if (!await _roleManager.RoleExistsAsync(Role.Customer))
                await _roleManager.CreateAsync(new IdentityRole(Role.Customer));

            await CreateUser("sys_admin", "sys_adminm@baseapp.com.br", "aBc@12345", Role.Admin);
            await CreateUser("customer", "customer@baseapp.com.br", "AbC@12345", Role.Customer);
        }

        private async Task CreateUser(string userName, string email, string password, string initialRole)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true
                };

                var resultado = await _userManager.CreateAsync(user, password);

                if (resultado.Succeeded)
                    await _userManager.AddToRoleAsync(user, initialRole);
            }
        }
    }
}