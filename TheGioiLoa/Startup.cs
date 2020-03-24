using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using TheGioiLoa.Models;

[assembly: OwinStartupAttribute(typeof(TheGioiLoa.Startup))]
namespace TheGioiLoa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "admin@thegioiloa.net",
                    Email = "admin@thegioiloa.net",
                    Birthday = DateTime.Now,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    FullName = "TheGioiLoa Admin",
                };
                string password = "admin@123";
                var createUser = UserManager.Create(user, password);

                if (createUser.Succeeded)
                {
                    UserManager.AddToRoleAsync(user.Id, "Admin");
                }
            }
        }
    }
}
