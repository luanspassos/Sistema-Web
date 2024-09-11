using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sales_Web_MVC.Data;
using Sales_Web_MVC.Services;
namespace Sales_Web_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Sales_Web_MVCContext>(options =>
                options.UseMySql(
                     builder.Configuration.GetConnectionString("Sales_Web_MVCContext"),
                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Sales_Web_MVCContext")),
                     mysqlOptions => mysqlOptions.MigrationsAssembly("Sales_Web_MVC")));

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<SellerService>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            var seedingService = app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>();
            seedingService.Seed();
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
