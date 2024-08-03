using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DpcontextApp: IdentityDbContext<AppUser>
    {
        public DpcontextApp(DbContextOptions<DpcontextApp> op ):base(op)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Catergory>().HasData(new { Catergory_id="1", Catergory_name="Action" },new { Catergory_id = "2", Catergory_name = "Romance" },new { Catergory_id = "3", Catergory_name = "Anime" });
        }
        public DbSet<Catergory> Catergories { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Post> Posts { get; set; }  
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RecomendCategories> RecomendCategories { get; set; }
        //public DbSet<WatchHistory>Watches { get; set; }

    }
}
