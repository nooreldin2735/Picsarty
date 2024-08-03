using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Picsarty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DpcontextApp>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDefaultIdentity<DpcontextApp>(op=>op.SignIn.RequireConfirmedAccount=true);
            builder.Services.AddDefaultIdentity<AppUser>().AddEntityFrameworkStores<DpcontextApp>().AddDefaultTokenProviders();
            //builder.Services.AddRazorPages();
            builder.Services.AddScoped<UnitOfWork>()
                .AddScoped<UnitOfWork>();
            builder.Services.AddRazorPages();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
                pattern: "{area=R_User}/{controller=Home}/{action=Index}/{id?}"

               );
            app.UseEndpoints(endpoints => endpoints.MapRazorPages());


            app.Run();
        }
    }
}
