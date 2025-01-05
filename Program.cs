using AutoPlaza.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        // Create and run the web application
        var builder = CreateBuilder(args);
        var app = builder.Build();
        Configure(app, builder.Environment);
        app.Run();
    }

    // CreateBuilder method - Service registration (DI, configuration)
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register services (like DbContext, MVC, etc.)
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddControllersWithViews(); // Add MVC services

        return builder;
    }

    // Configure method - Set up middleware, routing, etc.
    public static void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // Set up the default route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}
