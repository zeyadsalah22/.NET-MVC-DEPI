using Microsoft.EntityFrameworkCore;
using Day6Mydemo.Models;
using Day6Mydemo.Repositories.Implements;
using Day6Mydemo.Repositories.Interfaces;
using Day6Mydemo.CustomFilters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Day6Mydemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<Day6MvcdbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Day6MvcdbContext>();
            
            builder.Services.AddControllersWithViews(
                option => option.Filters.Add(new CustomActionFilter()));
            
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

            builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<Day6MvcdbContext>();
            //builder.Services.AddDbContext<Day6MvcdbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // add custom error page
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "demo",
                pattern: "demo/{id:range(10,50):int?}/{name:alpha?}",
                new
                {
                    controller = "Demos",
                    action = "Routing"
                });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=ShowWebsite}/{id?}");

            app.Run();
        }
    }
}
