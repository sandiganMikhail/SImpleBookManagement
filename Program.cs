using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SImpleBookManagement.Data;

namespace SImpleBookManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<IdentityOptions>(IdentityOptions =>
            {
                IdentityOptions.Password.RequireDigit = false;
                IdentityOptions.Password.RequireLowercase = false;
                IdentityOptions.Password.RequireNonAlphanumeric = false;
                IdentityOptions.Password.RequireUppercase = false;
                IdentityOptions.Password.RequiredLength = 2;
                IdentityOptions.Password.RequiredUniqueChars = 0;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
               var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
               var roles = new[] { "Admin", "Patron" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
                
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "admin@gmail.com";
                string password = "adminadmin";

                if (await userManager.FindByEmailAsync(email) == null)      
                {
                    var user = new IdentityUser
                    {
                        UserName = email,
                        Email = email
                    };

                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }

               
            }

            app.Run();
        }
    }
}