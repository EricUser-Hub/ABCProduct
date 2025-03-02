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

/*
        public virtual IEnumerable<TModel> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);
            
            foreach (var includeProperty in includeProperties.Split
                ([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            //if (orderBy != null)
            //    return [.. orderBy(query)];
            var products = query.ToList();
                
            return products.ForEach(p => new ProductModel(p.));
        }
        */

        public virtual async Task<ProductModel?> GetByDINAsync(object din)
        {
            ProductModel? returnValue = null;
            
            var product = await dbSet.FindAsync(din);
            if (product != null)
                returnValue = new ProductModel(product.DIN, product.Name, product.Shape, product.Strength, (LegalStatus)product.LegalStatus);
            
            return returnValue;
        }

        public void InsertOrUpdate(ProductModel model)
        {
            ProductEntity? entityToUpdate = dbSet.Find(model.DIN);
            if (entityToUpdate != null)
                dbSet.Remove(entityToUpdate);
                
            var entity = new ProductEntity() { DIN = model.DIN, Name = model.Name, Shape = model.Shape, Strength = model.Strength, LegalStatus = (int)model.LegalStatus };
            dbSet.Add(entity);
        }

        public virtual void Delete(string din)
        {
            ProductEntity? entityToDelete = dbSet.Find(din);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public virtual void Delete(ProductEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
        }

/*
        public virtual void Update(ProductEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        */
    }
}