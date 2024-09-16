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

            //builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
            //builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
            builder.Services.AddSession(config =>
            {
                config.IdleTimeout = TimeSpan.FromMinutes(30);
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

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=ShowWebsite}/{id?}");

            app.Run();
        }
    }
}
