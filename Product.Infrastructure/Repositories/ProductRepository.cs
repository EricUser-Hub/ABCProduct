using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Enums;
using Product.Domain.Model;
using Product.Infrastructure.Context;
using Product.Infrastructure.Entities;

namespace Product.Infrastructure.Repositories 
{
    public class ProductRepository 
    {
        internal ProductDbContext context;
        internal DbSet<ProductEntity> dbSet;

        public ProductRepository(ProductDbContext productContext)
        {
            context = productContext;
            dbSet = context.Set<ProductEntity>();
        }

        public virtual async Task<ProductModel?> GetByDINAsync(object din)
        {
            ProductModel? returnValue = null;
            
            var product = await dbSet.FindAsync(din);
            if (product != null)
                returnValue = new ProductModel(product.DIN, product.Name, product.Shape, product.Strength, (LegalStatus)product.LegalStatus);
            
            return returnValue;
        }

        public virtual async Task<ICollection<ProductModel>> GetAllFiltered(string? shapeFilter, LegalStatus? legalStatusFilter)
        {
            var result = dbSet.AsNoTracking();

            if (!string.IsNullOrEmpty(shapeFilter))
                result = result.Where(p => p.Shape == shapeFilter);
            if (legalStatusFilter.HasValue)
                result = result.Where(p => p.LegalStatus == (int)legalStatusFilter);
            
            var bag = new ConcurrentBag<ProductModel>();
            await result.ForEachAsync(product => bag.Add(new ProductModel(product.DIN, product.Name, product.Shape, product.Strength, (LegalStatus)product.LegalStatus)));
            
            return [.. bag];
        }

        public void InsertOrUpdate(ProductModel model)
        {
            ProductEntity? entityToUpdate = dbSet.Find(model.DIN);
            if (entityToUpdate != null)
                dbSet.Remove(entityToUpdate);
                
            var entity = new ProductEntity() { DIN = model.DIN, Name = model.Name, Shape = model.Shape, Strength = model.Strength, LegalStatus = (int)model.LegalStatus };
            dbSet.Add(entity);
        }

        public virtual void Delete(ProductEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
        }
    }
}