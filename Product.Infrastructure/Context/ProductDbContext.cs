using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Model;
using Product.Infrastructure.Entities;
using Product.Infrastructure.InitialSeed;

namespace Product.Infrastructure.Context
{
    public class ProductDbContext : DbContext
    {
        public string DbPath { get; }

        public ProductDbContext()
        {
            var path = 
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbPath = Path.Join(path, "ABCProduct.db");
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasKey(p => new { p.DIN });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}", x => x.MigrationsAssembly("Product.Migrations"))
                      .UseSeeding((context, _) =>
                        {
                            var anyProductExist = context.Set<ProductEntity>().Any();
                            if (!anyProductExist)
                            {                                
                                var sourceProducts = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(Seed.ProductsJson);

                                if (sourceProducts != null)
                                {
                                    foreach (var item in sourceProducts)
                                        context.Add(new ProductEntity() {DIN = item.DIN, Name = item.Name, Shape = item.Shape, Strength = item.Strength, LegalStatus = (int)item.LegalStatus});
                                        
                                    context.SaveChanges();
                                }   
                            }
                        });
    }
}