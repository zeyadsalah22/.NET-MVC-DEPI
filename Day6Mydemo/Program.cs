using Microsoft.EntityFrameworkCore;
using Day6Mydemo.Models;
namespace Day6Mydemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddAuthentication("Cookies")
            //    .AddCookie("Cookies", options =>
            //    {
            //        options.LoginPath = "/AuthUsers/Login";  // Path for login
            //        options.LogoutPath = "/AuthUsers/Logout";  // Path for logout
            //        options.ExpireTimeSpan = TimeSpan.FromDays(14);  // Default expiration time for cookies
            //        options.SlidingExpiration = true;  // Sliding expiration to renew cookie on each request
            //    });

            //builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
            //builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  // How long the session can be idle before timing out
                //options.Cookie.HttpOnly = true;  // Ensures cookie cannot be accessed by client-side scripts
                //options.Cookie.IsEssential = true;  // Marks the cookie as essential for GDPR compliance
            });


            builder.Services.AddDbContext<Day6MvcdbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=ShowWebsite}/{id?}");

            app.Run();
        }
    }
}
