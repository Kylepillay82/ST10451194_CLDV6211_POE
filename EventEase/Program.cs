using Microsoft.EntityFrameworkCore;
using EventEase.Models;
using Azure.Storage.Blobs;
using EventEase.Services; // assuming your BlobService is in this namespace

namespace EventEase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add EF Core DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register BlobService as a singleton (reads config internally)
            builder.Services.AddSingleton<BlobService>();

            var app = builder.Build();

            // Apply migrations and seed database BEFORE app.Run()
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                if (!context.EventTypes.Any())
                {
                    context.EventTypes.AddRange(
                        new EventType { EventTypeId = 1, Name = "Conference" },
                        new EventType { EventTypeId = 2, Name = "Wedding" },
                        new EventType { EventTypeId = 3, Name = "Concert" }
                    );
                    context.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

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
