using BakuSpirtis.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Advertisment> Advertisments { get; set; }
        public DbSet<Galery> Galeries { get; set; }
        public DbSet<Partners> Partners { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Corusel> Corusels { get; set; }
        public DbSet<CompanyVideo> CompanyVideos { get; set; }
        public DbSet<Sertification> Sertifications { get; set; }
    }
}
